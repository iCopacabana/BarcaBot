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

            using (var c = new FootballDataDatabaseConnection())
            {
                c.SetUclScorers(scorers);
            }
        }

        public async Task UpdateLaLigaScorers()
        {
            var scorers = await _retriever.GetScorers(FootballDataUrls.LaLigaId);

            using (var c = new FootballDataDatabaseConnection())
            {
                c.SetLaLigaScorers(scorers);
            }
        }

        public async Task UpdateMatches()
        {
            var matches = await _retriever.GetScheduledBarcaMatches();
            
            using (var c = new FootballDataDatabaseConnection())
            {
                c.SetMatches(matches);
            }
        }
    }
}