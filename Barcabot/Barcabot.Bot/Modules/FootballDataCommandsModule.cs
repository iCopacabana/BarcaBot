using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Barcabot.Common;
using Barcabot.Common.DataModels;
using Barcabot.Database;
using ConsoleTables;
using Discord;
using Discord.Commands;

namespace Barcabot.Bot.Modules
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class FootballDataCommandsModule : ModuleBase<SocketCommandContext>
    {
        [Command("table", RunMode = RunMode.Async)]
        public async Task Table([Remainder]string mode = "")
        {
            using (var c = new BarcabotDatabaseConnection())
            {
                var sqlTable = c.GetLaLigaStandings();
                var consoleTable = new ConsoleTable("", "Club", "MP", "W", "D", "L", "GD", "Pts");
                var m = mode.ToLower();
                
                if (m.Equals("top"))
                {
                    var t = sqlTable.Take(5).ToList();

                    Loop(t);
                }
                else if (m.Equals("bottom"))
                {
                    var t = sqlTable.TakeLast(5);
                    
                    Loop(t);
                }
                else
                {
                    var index = sqlTable.FindIndex(team => team.Team.ToLower().Contains(m));

                    if (index > 2)
                    {
                        index -= 2;
                    }
                    else
                    {
                        index = 0;
                    }

                    var t = sqlTable.Skip(index).Take(5);
                    
                    Loop(t);
                }

                await Context.Channel.SendMessageAsync("LaLiga Santander Table\n" + "```json\n" +
                                                       consoleTable.ToMinimalString() + "\nTable is updated every 60 seconds."
                                                       + "```");
                
                void Loop(IEnumerable<StandingsTeam> collection)
                {
                    foreach (var team in collection)
                    {
                        consoleTable.AddRow(team.Position, team.Team, team.Played, team.Won, team.Drawn, team.Lost, team.Gd,
                            team.Points);
                    }
                }
            }
        }

        [Command("laligascorers", RunMode = RunMode.Async)]
        public async Task LaLigaScorers()
        {
            using (var c = new BarcabotDatabaseConnection())
            {
                var scorersList = c.GetLaLigaScorersList();

                if (!scorersList.Any())
                {
                    await Context.Channel.SendMessageAsync(":warning: Come back when LaLiga season starts.");
                }
                else
                {
                    var builder = new EmbedBuilder();
                    builder.WithTitle("LaLiga Santander Top Scorers");
                    builder.WithThumbnailUrl("https://files.laliga.es/seccion_logos/laliga-v-600x600_2018.jpg");
                    builder.WithColor(Color.Gold);

                    foreach (var scorer in scorersList)
                    {
                        builder.AddField($"{scorer.ScorerId + 1}. {scorer.ScorerName}, {scorer.ScorerTeam}", $"Goals: {scorer.ScorerGoals}", false);
                    }
                    
                    await Context.Channel.SendMessageAsync("", false, builder.Build());
                }
            }
        }
        
        [Command("uclscorers", RunMode = RunMode.Async)]
        public async Task UclScorers()
        {
            using (var c = new BarcabotDatabaseConnection())
            {
                var scorersList = c.GetUclScorersList();

                if (!scorersList.Any())
                {
                    await Context.Channel.SendMessageAsync(":warning: Come back when UEFA Champions League season starts.");
                }
                else
                {
                    var builder = new EmbedBuilder();
                    builder.WithTitle("UEFA Champions League Top Scorers");
                    builder.WithThumbnailUrl("https://i.pinimg.com/originals/4e/e8/e9/4ee8e9139110201b6e17ac878d1250fd.jpg");
                    builder.WithColor(Color.Gold);

                    foreach (var scorer in scorersList)
                    {
                        builder.AddField($"{scorer.ScorerId + 1}. {scorer.ScorerName}, {scorer.ScorerTeam}", $"Goals: {scorer.ScorerGoals}", false);
                    }
                    
                    await Context.Channel.SendMessageAsync("", false, builder.Build());
                }
            }
        }
        
        [Command("schedule", RunMode = RunMode.Async)]
        public async Task Schedule()
        {
            using (var c = new BarcabotDatabaseConnection())
            {
                var scheduledMatchesList = c.GetScheduledMatchesList().Take(5).ToList();
                
                if (!scheduledMatchesList.Any())
                {
                    await Context.Channel.SendMessageAsync(":warning: Season ended. Come back later");
                }
                else
                {
                    var builder = new EmbedBuilder();
                    builder.WithTitle("Schedule");
                    builder.WithThumbnailUrl("https://upload.wikimedia.org/wikipedia/en/thumb/4/47/FC_Barcelona_%28crest%29.svg/1200px-FC_Barcelona_%28crest%29.svg.png");
                    builder.WithColor(Color.Purple);
                    
                    foreach (var match in scheduledMatchesList)
                    {
                        var date = DateConverter.ConvertToString(match.MatchDate);
                        
                        builder.AddField($"{match.MatchHomeTeam} - {match.MatchAwayTeam}", $"{match.MatchCompetition}, {date} UTC");
                    }

                    builder.WithFooter("To see info with more detail about next match do -nextmatch");
                    
                    await Context.Channel.SendMessageAsync("", false, builder.Build());
                }
            }
        }
        
        [Command("nextmatch", RunMode = RunMode.Async)]
        public async Task NextMatch()
        {
            using (var c = new BarcabotDatabaseConnection())
            {
                var scheduledMatchesList = c.GetScheduledMatchesList();
                
                if (!scheduledMatchesList.Any())
                {
                    await Context.Channel.SendMessageAsync(":warning: Season ended. Come back later");
                }
                else
                {
                    var match = scheduledMatchesList[0];
                    var builder = new EmbedBuilder();
                    var date = DateConverter.ConvertToDateTime(match.MatchDate);
                    var dateString = DateConverter.ConvertToString(match.MatchDate);
                    var timeUntil = (date - DateTime.Now);
                    var timeUntilString = $"{timeUntil.Days} days, {timeUntil.Hours} hours, {timeUntil.Minutes} minutes";
                    
                    builder.WithTitle("Next match");
                    builder.WithThumbnailUrl("https://upload.wikimedia.org/wikipedia/en/thumb/4/47/FC_Barcelona_%28crest%29.svg/1200px-FC_Barcelona_%28crest%29.svg.png");
                    builder.WithColor(Color.Purple);
                    builder.AddField($"{match.MatchHomeTeam} - {match.MatchAwayTeam}", $"{match.MatchCompetition} \n {match.MatchStadium}");
                    builder.AddField("Time", $"Kick-off time: {dateString} UTC\nTime until kick-off: {timeUntilString}");
                    builder.AddField("Head to Head - Recent games", $"Matches: {match.MatchTotalMatches} \n Wins: {match.MatchWins} \n Draws: {match.MatchDraws} \n Losses: {match.MatchLosses}");
                    
                    await Context.Channel.SendMessageAsync("", false, builder.Build());
                }
            }
        }
    }
}