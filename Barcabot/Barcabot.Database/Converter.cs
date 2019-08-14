using Barcabot.Common.DataModels;
using Barcabot.Common.DataModels.Dto.ApiFootball;
using Dribbles = Barcabot.Common.DataModels.Dribbles;
using Duels = Barcabot.Common.DataModels.Duels;
using Fouls = Barcabot.Common.DataModels.Fouls;
using Passes = Barcabot.Common.DataModels.Passes;
using SqlPlayer = Barcabot.Common.DataModels.Dto.Sql.Player;
using Player = Barcabot.Common.DataModels.Player;
using Shots = Barcabot.Common.DataModels.Shots;
using Tackles = Barcabot.Common.DataModels.Tackles;

namespace Barcabot.Database
{
    public static class Converter
    {
        public static SqlPlayer ToSqlPlayer(Player player)
        {
            return new SqlPlayer
            {
                Id = player.Id,
                Name = player.Name,
                Position = player.Position,
                Age = player.Age,
                Nationality = player.Nationality,
                Height = player.Height,
                Weight = player.Weight,
                Rating = player.Rating,
                ShotsTotal = player.Per90Stats.Shots.Total,
                ShotsOnTarget = player.Per90Stats.Shots.OnTarget,
                ShotsPercentageOnTarget = player.Per90Stats.Shots.PercentageOnTarget,
                PassesTotal = player.Per90Stats.Passes.Total,
                PassesKeyPasses = player.Per90Stats.Passes.KeyPasses,
                PassesAccuracy = player.Per90Stats.Passes.Accuracy,
                TacklesTotalTackles = player.Per90Stats.Tackles.TotalTackles,
                TacklesBlocks = player.Per90Stats.Tackles.Blocks,
                TacklesInterceptions = player.Per90Stats.Tackles.Interceptions,
                DuelsWon = player.Per90Stats.Duels.Won,
                DuelsPercentageWon = player.Per90Stats.Duels.PercentageWon,
                DribblesAttempted = player.Per90Stats.Dribbles.Attempted,
                DribblesWon = player.Per90Stats.Dribbles.Won,
                DribblesPercentageWon = player.Per90Stats.Dribbles.PercentageWon,
                FoulsDrawn = player.Per90Stats.Fouls.Drawn,
                FoulsCommitted = player.Per90Stats.Fouls.Committed,
                GoalsTotal = player.Goals.Total,
                GoalsConceded = player.Goals.Conceded,
                GoalsAssists = player.Goals.Assists
            };
        }

        public static Player FromSqlPlayer(SqlPlayer sqlPlayer)
        {
            return new Player
            {
                Id = sqlPlayer.Id,
                Name = sqlPlayer.Name,
                Position = sqlPlayer.Position,
                Age = sqlPlayer.Age,
                Nationality = sqlPlayer.Nationality,
                Height = sqlPlayer.Height,
                Weight = sqlPlayer.Weight,
                Rating = sqlPlayer.Rating,
                Per90Stats = new Per90Stats
                {
                    Shots = new Shots
                    {
                        Total = sqlPlayer.ShotsTotal,
                        OnTarget = sqlPlayer.ShotsOnTarget,
                        PercentageOnTarget = sqlPlayer.ShotsPercentageOnTarget
                    },
                    Passes = new Passes
                    {
                        Total = sqlPlayer.PassesTotal,
                        Accuracy = sqlPlayer.PassesAccuracy,
                        KeyPasses = sqlPlayer.PassesKeyPasses
                    },
                    Tackles = new Tackles
                    {
                        TotalTackles = sqlPlayer.TacklesTotalTackles,
                        Blocks = sqlPlayer.TacklesBlocks,
                        Interceptions = sqlPlayer.TacklesInterceptions
                    },
                    Duels = new Duels
                    {
                        Won = sqlPlayer.DuelsWon,
                        PercentageWon = sqlPlayer.DuelsPercentageWon
                    },
                    Dribbles = new Dribbles
                    {
                        Attempted = sqlPlayer.DribblesAttempted,
                        Won = sqlPlayer.DribblesWon,
                        PercentageWon = sqlPlayer.DribblesPercentageWon
                    },
                    Fouls = new Fouls
                    {
                        Drawn = sqlPlayer.FoulsDrawn,
                        Committed = sqlPlayer.FoulsCommitted
                    }
                },
                Goals = new Goals
                {
                    Total = sqlPlayer.GoalsTotal,
                    Conceded = sqlPlayer.GoalsConceded,
                    Assists = sqlPlayer.GoalsAssists
                }
            };
        }
    }
}