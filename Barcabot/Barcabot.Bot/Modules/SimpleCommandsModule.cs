using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Barcabot.Bot.Modules
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class SimpleCommandsModule : ModuleBase<SocketCommandContext>
    {
        [Command("help", RunMode = RunMode.Async)]
        public async Task Help()
        {
            var builder = new EmbedBuilder();
            builder.WithTitle("Help");
            builder.AddField("=help", "Lists all the available commands.");
            builder.AddField("=nextmatch", "Shows the next match FC Barcelona will play (in more detail than -schedule).");
            builder.AddField("=schedule", "Shows the upcoming games for FC Barcelona.");
            builder.AddField("=player *playername*", "Generates a player card with up to date stats (updated daily).");
            builder.AddField("=playerchart *player*", "Generates a stat chart with up to date date stats (updated daily).");
            builder.AddField("=playerschart *player1* *player2*", "Generates a stat chart with up to date date stats (updated daily) that compares stats of selected players.");
            builder.AddField("=laligascorers", "Shows up to date (updated every minute) top goal scorers for the LaLiga Santander.");
            builder.AddField("=uclscorers", "Shows up to date (updated every minute) top goal scorers for the UEFA Champions League.");
            builder.AddField("=feature", "Shows you how to request a feature.");
            builder.AddField("=github", "Shows a link to BarcaBot's main Github repo.");
            builder.AddField("=issue", "Shows you how to report an issue.");
            builder.WithThumbnailUrl("https://avatars1.githubusercontent.com/u/51497959?s=200&v=4");
            builder.WithColor(Color.Purple);

            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        
        [Command("feature", RunMode = RunMode.Async)]
        public async Task Feature()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("Suggest a feature");
            builder.AddField("To suggest a feature follow the link below and create a new issue with label `enhancement`:", "https://github.com/TraceLD/BarcaBot/issues/new \n\n You can also just DM the creator of BarcaBot `Trace#8994` on Discord.", true);
            builder.WithThumbnailUrl("https://github.githubassets.com/images/modules/logos_page/GitHub-Mark.png");
            builder.WithColor(Color.Green);
            
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        
        [Command("github", RunMode = RunMode.Async)]
        public async Task Github()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("GitHub");
            builder.AddField("Report issues, suggest features, give a star.", "https://github.com/TraceLD/BarcaBot", true);
            builder.WithThumbnailUrl("https://github.githubassets.com/images/modules/logos_page/GitHub-Mark.png");
            builder.WithColor(Color.Green);
            
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
        
        [Command("issue", RunMode = RunMode.Async)]
        public async Task Issue()
        {
            var builder = new EmbedBuilder();

            builder.WithTitle("Report an issue");
            builder.AddField("To report an issue follow the link below and create a new issue with label `bug`:", "https://github.com/TraceLD/BarcaBot/issues/new \n\n You can also just DM the creator of BarcaBot `Trace#8994` on Discord.", true);
            builder.WithThumbnailUrl("https://github.githubassets.com/images/modules/logos_page/GitHub-Mark.png");
            builder.WithColor(Color.Red);
            
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
    }
}