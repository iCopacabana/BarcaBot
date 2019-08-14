using System.Collections.Generic;
using SqlPlayer = Barcabot.Common.DataModels.Dto.Sql.Player;
using Player = Barcabot.Common.DataModels.Player;

namespace Barcabot.Database
{
    public interface IPlayersDatabaseConnection
    {
        void SetPlayer(Player player);
        void SetPlayers(List<Player> playersList);
        Player GetPlayerByName(string name);
        Player GetPlayerById(int id);
    }
}