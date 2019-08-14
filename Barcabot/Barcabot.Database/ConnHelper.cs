using Barcabot.Common;

namespace Barcabot.Database
{
    public static class ConnHelper
    {
        public static string GetConnectionString(string databaseName)
        {
            var config = YamlConfiguration.Config.Postgres;

            return $"Server=127.0.0.1;Port=5432;Database={databaseName};User Id={config.Username};Password={config.Password};";
        }
    }
}