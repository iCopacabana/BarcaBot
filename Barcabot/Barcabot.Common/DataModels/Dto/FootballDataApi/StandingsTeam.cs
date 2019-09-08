namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
    public class StandingsTeam
    {
        public int Position { get; set; }
        public StandingsTeamNested Team { get; set; }
        public int PlayedGames { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
    }
}