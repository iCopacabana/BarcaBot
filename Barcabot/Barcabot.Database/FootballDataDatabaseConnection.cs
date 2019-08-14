using System;
using System.Collections.Generic;
using System.Linq;
using Barcabot.Common;
using Barcabot.Common.DataModels;
using Dapper;
using Npgsql;

namespace Barcabot.Database
{
    public class FootballDataDatabaseConnection : DatabaseConnection, IDisposable, IFootballDataDatabaseConnection
    {
        public FootballDataDatabaseConnection()
        {
            Connection = new NpgsqlConnection(ConnHelper.GetConnectionString(YamlConfiguration.Config.Postgres.DatabaseNames.FootballData));
        }
        
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
        
        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
