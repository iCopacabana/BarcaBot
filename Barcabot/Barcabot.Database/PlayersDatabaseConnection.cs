using System;
using System.Collections.Generic;
using System.Linq;
using Barcabot.Common;
using Dapper;
using Npgsql;
using SqlPlayer = Barcabot.Common.DataModels.Dto.Sql.Player;
using Player = Barcabot.Common.DataModels.Player;

namespace Barcabot.Database
{
    public class PlayersDatabaseConnection : DatabaseConnection, IDisposable, IPlayersDatabaseConnection
    {
        public PlayersDatabaseConnection()
        {
            Connection = new NpgsqlConnection(ConnHelper.GetConnectionString(YamlConfiguration.Config.Postgres.DatabaseNames.Players));
        }
        
        public void SetPlayer(Player player)
        {
            var sqlPlayer = Converter.ToSqlPlayer(player);
            
            Connection.Execute(@"insert into player (id, name, position, age, nationality, height, weight, rating, shotstotal, shotsontarget, shotspercentageontarget, passestotal, passeskeypasses, passesaccuracy, tacklestotaltackles, tacklesblocks, tacklesinterceptions, duelswon, duelspercentagewon, dribblesattempted, dribbleswon, dribblespercentagewon, foulsdrawn, foulscommitted, goalstotal, goalsconceded, goalsassists) values (@id, @name, @position, @age, @nationality, @height, @weight, @rating, @shotstotal, @shotsontarget, @shotspercentageontarget, @passestotal, @passeskeypasses, @passesaccuracy, @tacklestotaltackles, @tacklesblocks, @tacklesinterceptions, @duelswon, @duelspercentagewon, @dribblesattempted, @dribbleswon, @dribblespercentagewon, @foulsdrawn, @foulscommitted, @goalstotal, @goalsconceded, @goalsassists) on conflict (id) do update set (id, name, position, age, nationality, height, weight, rating, shotstotal, shotsontarget, shotspercentageontarget, passestotal, passeskeypasses, passesaccuracy, tacklestotaltackles, tacklesblocks, tacklesinterceptions, duelswon, duelspercentagewon, dribblesattempted, dribbleswon, dribblespercentagewon, foulsdrawn, foulscommitted, goalstotal, goalsconceded, goalsassists) = (@id, @name, @position, @age, @nationality, @height, @weight, @rating, @shotstotal, @shotsontarget, @shotspercentageontarget, @passestotal, @passeskeypasses, @passesaccuracy, @tacklestotaltackles, @tacklesblocks, @tacklesinterceptions, @duelswon, @duelspercentagewon, @dribblesattempted, @dribbleswon, @dribblespercentagewon, @foulsdrawn, @foulscommitted, @goalstotal, @goalsconceded, @goalsassists) where player.id = @id", sqlPlayer);
        }
        
        public void SetPlayers(List<Player> playersList)
        {
            var sqlPlayersList = new List<SqlPlayer>();
            playersList.ForEach(el => sqlPlayersList.Add(Converter.ToSqlPlayer(el)));
            
            Connection.Execute(@"insert into player (id, name, position, age, nationality, height, weight, rating, shotstotal, shotsontarget, shotspercentageontarget, passestotal, passeskeypasses, passesaccuracy, tacklestotaltackles, tacklesblocks, tacklesinterceptions, duelswon, duelspercentagewon, dribblesattempted, dribbleswon, dribblespercentagewon, foulsdrawn, foulscommitted, goalstotal, goalsconceded, goalsassists) values (@id, @name, @position, @age, @nationality, @height, @weight, @rating, @shotstotal, @shotsontarget, @shotspercentageontarget, @passestotal, @passeskeypasses, @passesaccuracy, @tacklestotaltackles, @tacklesblocks, @tacklesinterceptions, @duelswon, @duelspercentagewon, @dribblesattempted, @dribbleswon, @dribblespercentagewon, @foulsdrawn, @foulscommitted, @goalstotal, @goalsconceded, @goalsassists) on conflict (id) do update set (id, name, position, age, nationality, height, weight, rating, shotstotal, shotsontarget, shotspercentageontarget, passestotal, passeskeypasses, passesaccuracy, tacklestotaltackles, tacklesblocks, tacklesinterceptions, duelswon, duelspercentagewon, dribblesattempted, dribbleswon, dribblespercentagewon, foulsdrawn, foulscommitted, goalstotal, goalsconceded, goalsassists) = (@id, @name, @position, @age, @nationality, @height, @weight, @rating, @shotstotal, @shotsontarget, @shotspercentageontarget, @passestotal, @passeskeypasses, @passesaccuracy, @tacklestotaltackles, @tacklesblocks, @tacklesinterceptions, @duelswon, @duelspercentagewon, @dribblesattempted, @dribbleswon, @dribblespercentagewon, @foulsdrawn, @foulscommitted, @goalstotal, @goalsconceded, @goalsassists) where player.id = @id", sqlPlayersList);
        }

        public Player GetPlayerByName(string playerName)
        {
            var parameter = $"%{playerName.Replace("%", "")}%";
            var output = Connection.Query<SqlPlayer>("SELECT * FROM player WHERE LOWER(name) LIKE @name LIMIT 1", new {name = parameter}).ToList();

            return Converter.FromSqlPlayer(output[0]);
        }
        
        public Player GetPlayerById(int playerId)
        {
            var output = Connection.Query<SqlPlayer>("select * from player where id = @id limit 1", new {id = playerId}).ToList();

            return Converter.FromSqlPlayer(output[0]);
        }
        
        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}