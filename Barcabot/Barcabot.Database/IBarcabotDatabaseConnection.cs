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
        List<StandingsTeam> GetLaLigaStandings();
        void SetLaLigaScorers(List<Scorer> data);
        void SetUclScorers(List<Scorer> data);
        void SetMatches(List<Match> data);
        void SetStandings(List<StandingsTeam> data);
        #endregion

        #region ApiFootballRelatedMethods
        void SetPlayer(Player player);
        void SetPlayers(List<Player> playersList);
        Player GetPlayerByName(string name);
        Player GetPlayerById(int id);
        #endregion
    }
}