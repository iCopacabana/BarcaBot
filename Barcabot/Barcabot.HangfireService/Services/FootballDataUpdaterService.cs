using System.Threading.Tasks;
using Barcabot.Database;
using Barcabot.Web;

namespace Barcabot.HangfireService.Services
{
    public class FootballDataUpdaterService : IFootballDataUpdaterService
    {
        private readonly FootballDataRetriever _retriever;

        public FootballDataUpdaterService(FootballDataRetriever retriever)
        {
            _retriever = retriever;
        }

        public async Task UpdateUclScorers()
        {
            var scorers = await _retriever.GetScorers(FootballDataUrls.UclId);

            using (var c = new BarcabotDatabaseConnection())
            {
                c.SetUclScorers(scorers);
            }
        }

        public async Task UpdateLaLigaScorers()
        {
            var scorers = await _retriever.GetScorers(FootballDataUrls.LaLigaId);

            using (var c = new BarcabotDatabaseConnection())
            {
                c.SetLaLigaScorers(scorers);
            }
        }

        public async Task UpdateMatches()
        {
            var matches = await _retriever.GetScheduledBarcaMatches();
            
            using (var c = new BarcabotDatabaseConnection())
            {
                c.SetMatches(matches);
            }
        }

        public async Task UpdateStandings()
        {
            var standings = await _retriever.GetLaLigaStandings();

            using (var c = new BarcabotDatabaseConnection())
            {
                c.SetStandings(standings);
            }
        }
    }
}