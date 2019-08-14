using System.Collections.Generic;
using System.Threading.Tasks;
using Match = Barcabot.Common.DataModels.Match;
using Scorer = Barcabot.Common.DataModels.Scorer;

namespace Barcabot.Web
{
    public interface IFootballDataRetriever
    {
        Task<List<Scorer>> GetScorers(string competitionId);
        Task<List<Match>> GetScheduledBarcaMatches();
    }
}