namespace Barcabot.Common.DataModels
{
    public class Match
    {
        public int MatchId { get; set; }
        public string MatchCompetition { get; set; }
        public string MatchDate { get; set; }
        public string MatchStadium { get; set; }
        public string MatchHomeTeam { get; set; }
        public string MatchAwayTeam { get; set; }
        public int MatchTotalMatches { get; set; }
        public int MatchTotalGoals { get; set; }
        public int MatchWins { get; set; }
        public int MatchDraws { get; set; }
        public int MatchLosses { get; set; }
    }
}