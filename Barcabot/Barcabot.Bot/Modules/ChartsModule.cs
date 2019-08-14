using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Barcabot.Common.DataModels;
using Barcabot.Database;
using Barcabot.Web;
using Discord.Commands;

namespace Barcabot.Bot.Modules
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ChartsModule : ModuleBase<SocketCommandContext>
    {

        private readonly PostService _postService;

        public ChartsModule(PostService postService)
        {
            _postService = postService;
        }

        [Command("playerchart", RunMode = RunMode.Async)]
        public async Task PlayerChart(string name)
        {
            using (var c = new PlayersDatabaseConnection())
            {
                try
                {
                    var playerObject = c.GetPlayerByName(name);
                    
                    
                    var postResponse = await _postService.GetStreamFromPost("http://localhost:3000/stats/player", playerObject);

                    if (postResponse.ResponseCode != "OK")
                    {
                        await Context.Channel.SendMessageAsync(
                            $":warning: Server Error: Response Code: {postResponse.ResponseCode}");
                    }
                    else
                    {
                        var stream = postResponse.ResponseContent;
                        
                        stream.Seek(0, SeekOrigin.Begin);
                        
                        await Context.Channel.SendFileAsync(stream, "chart.png");
                    }
                }
                catch(ArgumentOutOfRangeException)
                {
                    await Context.Channel.SendMessageAsync(
                        $":warning: Error: Could not find player `{name}`. Are you sure they exist and are a FCB player?\nIf you think there is a player missing from the database please report it to the creator of BarcaBot `Trace#8994`.");
                }
            }
        }
        
        [Command("playerschart", RunMode = RunMode.Async)]
        public async Task PlayersChart(string name1, string name2)
        {
            using (var c = new PlayersDatabaseConnection())
            {
                try
                {
                    var playerObject1 = c.GetPlayerByName(name1);

                    try
                    {
                        var playerObject2 = c.GetPlayerByName(name2);

                        var request = new
                        {
                            PlayerList = new List<Player>() {playerObject1, playerObject2}
                        };
                        
                        var postResponse = await _postService.GetStreamFromPost("http://localhost:3000/stats/players", request);

                        if (postResponse.ResponseCode != "OK")
                        {
                            await Context.Channel.SendMessageAsync(
                                $":warning: Server Error: Response Code: {postResponse.ResponseCode}");
                        }
                        else
                        {
                            var stream = postResponse.ResponseContent;
                        
                            stream.Seek(0, SeekOrigin.Begin);
                        
                            await Context.Channel.SendFileAsync(stream, "chart.png");
                        }
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        await Context.Channel.SendMessageAsync(
                            $":warning: Error: Could not find player `{name2}`. Are you sure they exist and are a FCB player?\nIf you think there is a player missing from the database please report it to the creator of BarcaBot `Trace#8994`.");
                    }
                }
                catch(ArgumentOutOfRangeException)
                {
                    await Context.Channel.SendMessageAsync(
                        $":warning: Error: Could not find player `{name1}`. Are you sure they exist and are a FCB player?\nIf you think there is a player missing from the database please report it to the creator of BarcaBot `Trace#8994`.");
                }
            }
        }
    }
}


