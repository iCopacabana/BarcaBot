using System.Threading.Tasks;
using Barcabot.Database;
using Barcabot.Web;

namespace Barcabot.HangfireService.Services
{
    public class PlayerUpdaterService : IPlayerUpdaterService
    {
        private readonly PlayerRetriever _retriever;

        public PlayerUpdaterService(PlayerRetriever retriever)
        {
            _retriever = retriever;
        }

        public async Task UpdatePlayers()
        {
            var players = await _retriever.Retrieve();

            using (var c = new PlayersDatabaseConnection())
            {
                c.SetPlayers(players);
            }
        }
    }
}