using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Barcabot.Common;
using Barcabot.Common.DataModels;
using Barcabot.Database;
using Barcabot.Web;
using Discord.Commands;
using Newtonsoft.Json;
using TraceLd.PlotlySharp;
using TraceLd.PlotlySharp.Api;
using TraceLd.PlotlySharp.Api.Traces;

namespace Barcabot.Bot.Modules
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class ChartsModule : ModuleBase<SocketCommandContext>
    {
        private static PlotlyCredentials _credProvider() => new PlotlyCredentials { Username = YamlConfiguration.Config.Plotly.Username, Token = YamlConfiguration.Config.Plotly.Token };
        // ReSharper disable once IdentifierTypo
        private readonly PlotlyClient _plotlyClient;
        
        public ChartsModule(HttpClient client)
        {
            _plotlyClient = new PlotlyClient(client, _credProvider);
        }
        
        [Command("playerchart", RunMode = RunMode.Async)]
        public async Task PlayerChart(string name)
        {
            using (var c = new PlayersDatabaseConnection())
            {
                var playerObject = c.GetPlayerByName(name);

                if (playerObject == null)
                {
                    await Context.Channel.SendMessageAsync(
                        $":warning: Error: Could not find player `{name}`. Are you sure they exist and are a FCB player?\nIf you think there is a player missing from the database please report it to the creator of BarcaBot `Trace#8994`.");
                }
                else
                {
                    var convertedName = NameConverter.ConvertName(playerObject.Name);
                    var stats = playerObject.Per90Stats;
                    var x = new ArrayList { "Shots", "Shots on Target", "Key Passes", "Tackles", "Blocks", "Interceptions", "Duels Won", "Dribbles Attempted", "Dribbles Won", "Fouls Drawn", "Fouls Committed" };
                    var y = new ArrayList { stats.Shots.Total, stats.Shots.OnTarget, stats.Passes.KeyPasses, stats.Tackles.TotalTackles, stats.Tackles.Blocks, stats.Tackles.Interceptions, stats.Duels.Won, stats. Dribbles.Attempted, stats.Dribbles.Won, stats.Fouls.Drawn, stats.Fouls.Committed };

                    var chart = new PlotlyChart
                    {
                        Figure = new Figure
                        {
                            Data = new ArrayList { new BarTrace
                            {
                                X = x,
                                Y = y,
                                Name = convertedName
                            }},
                            Layout = GetLayout($"{convertedName} Per 90 Stats", false)
                        },
                        Height = 500,
                        Width = 1000
                    };
                    
                    var chartAsBytes = await _plotlyClient.GetChartAsByteArray(chart);
                    var stream = new MemoryStream(chartAsBytes);
                        
                    await Context.Channel.SendFileAsync(stream, "chart.png");
                }
            }
        }
        
        [Command("playerschart", RunMode = RunMode.Async)]
        public async Task PlayersChart(string name1, string name2)
        {
            using (var c = new PlayersDatabaseConnection())
            {
                var playerObject1 = c.GetPlayerByName(name1);

                if (playerObject1 == null)
                {
                    await Context.Channel.SendMessageAsync(
                        $":warning: Error: Could not find player `{name1}`. Are you sure they exist and are a FCB player?\nIf you think there is a player missing from the database please report it to the creator of BarcaBot `Trace#8994`.");
                }
                else
                {
                    var playerObject2 = c.GetPlayerByName(name2);

                    if (playerObject2 == null)
                    {
                        await Context.Channel.SendMessageAsync(
                            $":warning: Error: Could not find player `{name2}`. Are you sure they exist and are a FCB player?\nIf you think there is a player missing from the database please report it to the creator of BarcaBot `Trace#8994`.");
                    }
                    else
                    {
                        var convertedName1 = NameConverter.ConvertName(playerObject1.Name);
                        var convertedName2 = NameConverter.ConvertName(playerObject2.Name);
                        var stats1 = playerObject1.Per90Stats;
                        var stats2 = playerObject2.Per90Stats;
                        var x = new ArrayList { "Shots", "Shots on Target", "Key Passes", "Tackles", "Blocks", "Interceptions", "Duels Won", "Dribbles Attempted", "Dribbles Won", "Fouls Drawn", "Fouls Committed" };
                        var y1 = new ArrayList { stats1.Shots.Total, stats1.Shots.OnTarget, stats1.Passes.KeyPasses, stats1.Tackles.TotalTackles, stats1.Tackles.Blocks, stats1.Tackles.Interceptions, stats1.Duels.Won, stats1.Dribbles.Attempted, stats1.Dribbles.Won, stats1.Fouls.Drawn, stats1.Fouls.Committed };
                        var y2 = new ArrayList { stats2.Shots.Total, stats2.Shots.OnTarget, stats2.Passes.KeyPasses, stats2.Tackles.TotalTackles, stats2.Tackles.Blocks, stats2.Tackles.Interceptions, stats2.Duels.Won, stats2.Dribbles.Attempted, stats2.Dribbles.Won, stats2.Fouls.Drawn, stats2.Fouls.Committed };
                        
                        var chart = new PlotlyChart
                        {
                            Figure = new Figure
                            {
                                Data = new ArrayList { new BarTrace
                                {
                                    X = x,
                                    Y = y1,
                                    Name = convertedName1
                                }, new BarTrace
                                {
                                    X = x,
                                    Y = y2,
                                    Name = convertedName2
                                }},
                                Layout = GetLayout($"{convertedName1} vs {convertedName2} Per 90 Stats", true)
                            },
                            Height = 500,
                            Width = 1000
                        };
                        
                        var chartAsBytes = await _plotlyClient.GetChartAsByteArray(chart);
                        var stream = new MemoryStream(chartAsBytes);
                        
                        await Context.Channel.SendFileAsync(stream, "chart.png");
                    }
                }
            }
        }
        
        private static Layout GetLayout(string text, bool showLegend)
        {
            return new Layout
            {
                Colorway = new List<string>
                {
                    "#636efa",
                    "#EF553B",
                    "#00cc96",
                    "#ab63fa",
                    "#19d3f3",
                    "#e763fa",
                    "#fecb52",
                    "#ffa15a",
                    "#ff6692",
                    "#b6e880"
                },
                Title = new Title
                {
                    Text = text
                },
                PaperBgColor = "#1a1a23",
                PlotBgColor = "#1a1a23",
                Font = new Font
                {
                    Color = "#ebebeb"
                },
                ShowLegend = showLegend
            };
        }
    }
}


