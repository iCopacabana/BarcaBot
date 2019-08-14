namespace Barcabot.Web
{
    public static class ApiFootballUrls
    {
        private const string RootUrl = "https://api-football-v1.p.rapidapi.com/v2";
        
        //e.g. https://api-football-v1.p.rapidapi.com/v2/players/team/529/2018-2019
        public static string BarcelonaPlayers(string seasonYearDashYear) => $"{RootUrl}/players/team/529/{seasonYearDashYear}";
    }
}