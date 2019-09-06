// ReSharper disable IdentifierTypo

namespace Barcabot.Common.DataModels.Config
{
    public class Config
    {
        public Postgres Postgres { get; set; }
        public Apis Apis { get; set; }
        public Plotly Plotly { get; set; }
        public string DiscordToken { get; set; }
    }
}