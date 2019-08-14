using System.Threading.Tasks;

namespace Barcabot.HangfireService.Services
{
    public interface IFootballDataUpdaterService
    {
        Task UpdateUclScorers();
        Task UpdateLaLigaScorers();
        Task UpdateMatches();
    }
}