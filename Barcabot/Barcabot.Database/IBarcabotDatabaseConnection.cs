using System.Collections.Generic;
using Barcabot.Common.DataModels;

namespace Barcabot.Database
{
    public interface IBarcabotDatabaseConnection
    {
        #region FootballDataRelatedMethods
        List<Scorer> GetLaLigaScorersList();
        List<Scorer> GetUclScorersList();
        List<Match> GetScheduledMatchesList();
        void SetLaLigaScorers(List<Scorer> data);
        void SetUclScorers(List<Scorer> data);
        void SetMatches(List<Match> data);
        #endregion

        #region ApiFootballRelatedMethods
        void SetPlayer(Player player);
        void SetPlayers(List<Player> playersList);
        Player GetPlayerByName(string name);
        Player GetPlayerById(int id);
        #endregion
    }
}