using System;
using System.Collections.Generic;
using System.Linq;
using Barcabot.Common;
using Barcabot.Common.DataModels;
using Dapper;
using Npgsql;
using SqlPlayer = Barcabot.Common.DataModels.Dto.Sql.Player;
using Player = Barcabot.Common.DataModels.Player;

namespace Barcabot.Database
{
    public class BarcabotDatabaseConnection : DatabaseConnection, IDisposable, IBarcabotDatabaseConnection
    {
        public BarcabotDatabaseConnection()
        {
            Connection = new NpgsqlConnection(ConnHelper.GetConnectionString(YamlConfiguration.Config.Postgres.DatabaseNames.Barcabot));
        }
        
        #region FootballDataRelatedMethods
        public List<Scorer> GetLaLigaScorersList()
        {
            var output = Connection.Query<Scorer>("select * from laligascorers order by scorerid asc").ToList();

            return output;
        }
        
        public List<Scorer> GetUclScorersList()
        {
            var output = Connection.Query<Scorer>("select * from uclscorers order by scorerid asc").ToList();

            return output;
        }
        
        public List<Match> GetScheduledMatchesList()
        {
            var output = Connection.Query<Match>("select * from matches order by matchid asc limit 5").ToList();

            return output;
        }
        
        public void SetLaLigaScorers(List<Scorer> data)
        {
            Connection.Execute(@"insert into laligascorers (scorerid, scorername, scorerteam, scorergoals) values (@scorerid, @scorername, @scorerteam, @scorergoals) on conflict (scorerid) do update set (scorerid, scorername, scorerteam, scorergoals) = (@scorerid, @scorername, @scorerteam, @scorergoals)", data);
        }
        
        public void SetUclScorers(List<Scorer> data)
        {
            Connection.Execute(@"insert into uclscorers (scorerid, scorername, scorerteam, scorergoals) values (@scorerid, @scorername, @scorerteam, @scorergoals) on conflict (scorerid) do update set (scorerid, scorername, scorerteam, scorergoals) = (@scorerid, @scorername, @scorerteam, @scorergoals)", data);
        }
        
        public void SetMatches(List<Match> data)
        {
            Connection.Execute(@"insert into matches (matchid, matchcompetition, matchdate, matchstadium, matchhometeam, matchawayteam, matchtotalmatches, matchtotalgoals, matchwins, matchdraws, matchlosses) values (@matchid, @matchcompetition, @matchdate, @matchstadium, @matchhometeam, @matchawayteam, @matchtotalmatches, @matchtotalgoals, @matchwins, @matchdraws, @matchlosses) on conflict (matchid) do update set (matchid, matchcompetition, matchdate, matchstadium, matchhometeam, matchawayteam, matchtotalmatches, matchtotalgoals, matchwins, matchdraws, matchlosses) = (@matchid, @matchcompetition, @matchdate, @matchstadium, @matchhometeam, @matchawayteam, @matchtotalmatches, @matchtotalgoals, @matchwins, @matchdraws, @matchlosses)", data);
        }
        #endregion
        
        #region ApiFootballRelatedMethods
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

            return !output.Any() ? null : Converter.FromSqlPlayer(output[0]);
        }
        
        public Player GetPlayerById(int playerId)
        {
            var output = Connection.Query<SqlPlayer>("select * from player where id = @id limit 1", new {id = playerId}).ToList();

            return Converter.FromSqlPlayer(output[0]);
        }
        #endregion
        
        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}