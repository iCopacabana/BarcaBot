using System.Threading.Tasks;

namespace Barcabot.HangfireService.Services
{
    public interface IPlayerUpdaterService
    {
        Task UpdatePlayers();
    }
}