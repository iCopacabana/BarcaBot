using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Barcabot.Common.DataModels;
using Barcabot.Database;
using Barcabot.Web;
using Discord.Commands;

namespace Barcabot.Bot.Modules
{
    public class PlayerCardsModule : ModuleBase<SocketCommandContext>
    {
        private readonly PostService _postService;

        public PlayerCardsModule(PostService postService)
        {
            _postService = postService;
        }

        [Command("player", RunMode = RunMode.Async)]
        public async Task Player(string name)
        {
            using (var c = new PlayersDatabaseConnection())
            {
                try
                {
                    var playerObject = c.GetPlayerByName(name);

                    var position = playerObject.Position == "Goalkeeper" ? "goalie" : playerObject.Position.ToLower();
                    
                    var postResponse = await _postService.GetStreamFromPost($"http://localhost:4000/player_cards/{position}/", playerObject);

                    if (postResponse.ResponseCode != "OK")
                    {
                        await Context.Channel.SendMessageAsync(
                            $":warning: Server Error: Response Code: {postResponse.ResponseCode}");
                    }
                    else
                    {
                        var stream = postResponse.ResponseContent;
                        
                        stream.Seek(0, SeekOrigin.Begin);
                        
                        await Context.Channel.SendFileAsync(stream, "card.png");
                    }
                }
                catch(ArgumentOutOfRangeException)
                {
                    await Context.Channel.SendMessageAsync(
                        $":warning: Error: Could not find player `{name}`. Are you sure they exist and are a FCB player?\nIf you think there is a player missing from the database please report it to the creator of BarcaBot `Trace#8994`.");
                }
            }
        }
    }
}