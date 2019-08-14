using System.Collections.Generic;
using Barcabot.Common.DataModels;

namespace Barcabot.Database
{
    public interface IFootballDataDatabaseConnection
    {
        List<Scorer> GetLaLigaScorersList();
        List<Scorer> GetUclScorersList();
        List<Match> GetScheduledMatchesList();
        void SetLaLigaScorers(List<Scorer> data);
        void SetUclScorers(List<Scorer> data);
        void SetMatches(List<Match> data);
    }
}