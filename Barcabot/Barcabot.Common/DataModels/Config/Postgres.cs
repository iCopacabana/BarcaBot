// ReSharper disable IdentifierTypo

namespace Barcabot.Common.DataModels.Config
{
    public class Postgres
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DatabaseNames DatabaseNames { get; set; }
    }
}