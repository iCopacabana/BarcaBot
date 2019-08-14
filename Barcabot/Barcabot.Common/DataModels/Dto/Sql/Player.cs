namespace Barcabot.Common.DataModels.Dto.Sql
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public double? Rating { get; set; }
        public double ShotsTotal { get; set; }
        public double ShotsOnTarget { get; set; }
        public double ShotsPercentageOnTarget { get; set; }
        public double PassesTotal { get; set; }
        public double PassesKeyPasses { get; set; }
        public double PassesAccuracy { get; set; }
        public double TacklesTotalTackles { get; set; }
        public double TacklesBlocks { get; set; }
        public double TacklesInterceptions { get; set; }
        public double DuelsWon { get; set; }
        public double DuelsPercentageWon { get; set; }
        public double DribblesAttempted { get; set; }
        public double DribblesWon { get; set; }
        public double DribblesPercentageWon { get; set; }
        public double FoulsDrawn { get; set; }
        public double FoulsCommitted { get; set; }
        public int GoalsTotal { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalsAssists { get; set; }
    }
}