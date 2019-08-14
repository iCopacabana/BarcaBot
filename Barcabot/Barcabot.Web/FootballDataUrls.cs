namespace Barcabot.Web
{
    public static class FootballDataUrls
    {
        private const string RootUrl = "https://api.football-data.org/v2/";
        public static string Match(string matchId) => $"{RootUrl}/matches/{matchId}";
        public static string ScheduledMatches(string teamId) => $"{RootUrl}/teams/{teamId}/matches?status=SCHEDULED";
        public static string TopScorers(string competitionId) => $"{RootUrl}/competitions/{competitionId}/scorers";
        public static string LaLigaId => "2014";
        public static string UclId => "2001";
    }
}