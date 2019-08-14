using Newtonsoft.Json;

namespace Barcabot.Common.DataModels.Dto.ApiFootball
{
    public partial class Games
    {
        // ReSharper disable once IdentifierTypo
        public int Appearences { get; set; }
        [JsonProperty("minutes_played")]
        public int MinutesPlayed { get; set; }
        public int Lineups { get; set; }
    }
}