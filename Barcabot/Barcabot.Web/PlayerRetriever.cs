using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Barcabot.Common;
using Barcabot.Common.DataModels;
using Barcabot.Common.DataModels.Dto.ApiFootball;
using Dribbles = Barcabot.Common.DataModels.Dto.ApiFootball.Dribbles;
using Duels = Barcabot.Common.DataModels.Dto.ApiFootball.Duels;
using Fouls = Barcabot.Common.DataModels.Dto.ApiFootball.Fouls;
using Passes = Barcabot.Common.DataModels.Dto.ApiFootball.Passes;
using Shots = Barcabot.Common.DataModels.Dto.ApiFootball.Shots;
using Tackles = Barcabot.Common.DataModels.Dto.ApiFootball.Tackles;

namespace Barcabot.Web
{
    public class PlayerRetriever : IPlayerRetriever
    {
        private readonly ApiFootballRetrievalService _service;
        private TeamResponse _response;

        public PlayerRetriever(ApiFootballRetrievalService service)
        {
            _service = service;
        }

        private async Task GetDto()
        {
            _response = await _service.RetrieveData<TeamResponse>(ApiFootballUrls.BarcelonaPlayers("2018-2019"));
        }

        private IEnumerable<FootballPlayer> GetListWithoutDuplicates()
        {
            var playerList = _response.Api.Players;
            var playerNameList = new List<string>();
            
            playerList.ForEach(el => playerNameList.Add(el.PlayerName));
            
            var playerNameListDistinct = playerNameList.Distinct();
            var playerListDistinct = new List<FootballPlayer>();
            
            foreach (var name in playerNameListDistinct)
            {
                var list = playerList.Where(player => player.PlayerName == name).ToList();
                
                var distinctPlayer = new FootballPlayer
                {
                    PlayerId = list[0].PlayerId,
                    PlayerName = list[0].PlayerName,
                    FirstName = list[0].FirstName,
                    LastName = list[0].LastName,
                    Number = list[0].Number,
                    Position = list[0].Position,
                    Age = list[0].Age,
                    BirthDate = list[0].BirthDate,
                    BirthCountry = list[0].BirthCountry,
                    BirthPlace = list[0].BirthPlace,
                    Nationality = list[0].Nationality,
                    Height = list[0].Height,
                    Weight = list[0].Weight,
                    Injured = list[0].Injured,
                    TeamId = list[0].TeamId,
                    TeamName = list[0].TeamName,
                    League = "La Liga",
                    Season = null,
                    Captain = null
                };

                var shots = new Shots();
                var goals = new Goals();
                var passes = new Passes {Accuracy = list[0].Passes.Accuracy};
                var tackles = new Tackles();
                var duels = new Duels();
                var dribbles = new Dribbles();
                var fouls = new Fouls();
                var cards = new Cards();
                var games = new Games();
                var subs = new Substitutes();
                var rating = new List<double>();
                
                foreach (var player in list)
                {
                    if (player.Rating != null) rating.Add((double) player.Rating);
                    shots.Total += player.Shots.Total;
                    shots.On += player.Shots.On;
                    goals.Total += player.Goals.Total;
                    goals.Assists += player.Goals.Assists;
                    goals.Conceded += player.Goals.Conceded;
                    passes.Total += player.Passes.Total;
                    passes.Key += player.Passes.Key;
                    tackles.Total += player.Tackles.Total;
                    tackles.Blocks += player.Tackles.Blocks;
                    tackles.Interceptions += player.Tackles.Interceptions;
                    duels.Total += player.Duels.Total;
                    duels.Won += player.Duels.Won;
                    dribbles.Attempts += player.Dribbles.Attempts;
                    dribbles.Success += player.Dribbles.Success;
                    fouls.Drawn += player.Fouls.Drawn;
                    fouls.Committed += player.Fouls.Committed;
                    cards.Red += player.Cards.Red;
                    cards.Yellow += player.Cards.Yellow;
                    cards.Yellowred += player.Cards.Yellowred;
                    games.Appearences += player.Games.Appearences;
                    games.Lineups += player.Games.Lineups;
                    games.MinutesPlayed += player.Games.MinutesPlayed;
                    subs.Bench += player.Substitutes.Bench;
                    subs.In += player.Substitutes.In;
                    subs.Out += player.Substitutes.Out;
                }

                try
                {
                    distinctPlayer.Rating = rating.Average();
                }
                catch
                {
                    // ignored
                }

                distinctPlayer.Shots = shots;
                distinctPlayer.Goals = goals;
                distinctPlayer.Passes = passes;
                distinctPlayer.Tackles = tackles;
                distinctPlayer.Duels = duels;
                distinctPlayer.Dribbles = dribbles;
                distinctPlayer.Fouls = fouls;
                distinctPlayer.Cards = cards;
                distinctPlayer.Games = games;
                distinctPlayer.Substitutes = subs;
                
                playerListDistinct.Add(distinctPlayer);
            }

            return playerListDistinct;
        }

        private List<Player> TransformFromDtoToDataModel(IEnumerable<FootballPlayer> dto)
        {
            var playerList = new List<Player>();
            
            foreach (var dtoPlayer in dto)
            {
                var rating = dtoPlayer.Rating != null ? Math.Round((double) dtoPlayer.Rating, 2) : 0.00;
                
                var player = new Player
                {
                    Id = dtoPlayer.PlayerId,
                    Name = StringNormalizer.Normalize(dtoPlayer.PlayerName),
                    Position = dtoPlayer.Position,
                    Age = dtoPlayer.Age,
                    Nationality = dtoPlayer.Nationality,
                    Height = dtoPlayer.Height,
                    Weight = dtoPlayer.Weight,
                    Rating = rating,
                    Per90Stats = new Per90Stats
                    {
                        Shots = new Common.DataModels.Shots
                        {
                            Total = ToPer90(dtoPlayer.Shots.Total),
                            OnTarget = ToPer90(dtoPlayer.Shots.On),
                            PercentageOnTarget = Percentage(dtoPlayer.Shots.On, dtoPlayer.Shots.Total)
                        },
                        Passes = new Common.DataModels.Passes
                        {
                            Total = ToPer90(dtoPlayer.Passes.Total),
                            Accuracy = dtoPlayer.Passes.Accuracy,
                            KeyPasses = ToPer90(dtoPlayer.Passes.Key)
                        },
                        Tackles = new Common.DataModels.Tackles
                        {
                            TotalTackles = ToPer90(dtoPlayer.Tackles.Total),
                            Blocks = ToPer90(dtoPlayer.Tackles.Blocks),
                            Interceptions = ToPer90(dtoPlayer.Tackles.Interceptions)
                        },
                        Duels = new Common.DataModels.Duels
                        {
                            Won = ToPer90(dtoPlayer.Duels.Won),
                            PercentageWon = Percentage(dtoPlayer.Duels.Won, dtoPlayer.Duels.Total)
                            
                        },
                        Dribbles = new Common.DataModels.Dribbles
                        {
                            Attempted = ToPer90(dtoPlayer.Dribbles.Attempts),
                            Won = ToPer90(dtoPlayer.Dribbles.Success),
                            PercentageWon = Percentage(dtoPlayer.Dribbles.Success, dtoPlayer.Dribbles.Attempts)
                        },
                        Fouls = new Common.DataModels.Fouls
                        {
                            Committed = ToPer90(dtoPlayer.Fouls.Committed),
                            Drawn = ToPer90(dtoPlayer.Fouls.Drawn)
                        }
                    },
                    Goals = dtoPlayer.Goals
                };
                
                playerList.Add(player);
                
                double ToPer90(int stat)
                {
                    // example
                    // (6705 passes / 18928 mins) x 90 = 31.9
                    
                    if (dtoPlayer.Games.MinutesPlayed == 0) return 0;
                    return Math.Round((double)stat / (double)dtoPlayer.Games.MinutesPlayed * 90, 2);
                }

                double Percentage(double statSmall, double statTotal)
                {
                    // example
                    // 10/20 * 100 = 50%
                    
                    try
                    {
                        return Math.Round(statSmall / statTotal * 100, 2);
                    }
                    catch (DivideByZeroException)
                    {
                        return 0;
                    }
                }
            }

            return playerList;
        }
        
        public async Task<List<Player>> Retrieve()
        {
            await GetDto();
            
            return TransformFromDtoToDataModel(GetListWithoutDuplicates());
        }
    }
}