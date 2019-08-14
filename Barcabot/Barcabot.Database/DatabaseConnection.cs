using System.Collections.Generic;
using System.Linq;
using Dapper;
using Npgsql;

namespace Barcabot.Database
{
    public abstract class DatabaseConnection
    {
        protected NpgsqlConnection Connection { get; set; }
        
        public void ExecuteRawNonQuery(string sql)
        {
            Connection.Execute(sql);
        }

        public void ExecuteRawNonQuery<T>(string sql, T objectToInsert)
        {
            Connection.Execute(sql, objectToInsert);
        }

        public List<T> ExecuteRawQuery<T>(string sql)
        {
            return Connection.Query<T>(sql).ToList();
        }
    }
}