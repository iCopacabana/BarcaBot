using Newtonsoft.Json;

namespace Barcabot.Common.DataModels.Dto.ApiFootball
{
    public class FootballPlayer
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }
        [JsonProperty("player_name")]
        public string PlayerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Number { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }
        [JsonProperty("birth_place")]
        public string BirthPlace { get; set; }
        [JsonProperty("birth_country")]
        public string BirthCountry { get; set; }
        public string Nationality { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Injured { get; set; }
        public double? Rating { get; set; }
        [JsonProperty("team_id")]
        public int TeamId { get; set; }
        [JsonProperty("team_name")]
        public string TeamName { get; set; }
        public string League { get; set; }
        public string Season { get; set; }
        public int? Captain { get; set; }
        public Shots Shots { get; set; }
        public Goals Goals { get; set; }
        public Passes Passes { get; set; }
        public Tackles Tackles { get; set; }
        public Duels Duels { get; set; }
        public Dribbles Dribbles { get; set; }
        public Fouls Fouls { get; set; }
        public Cards Cards { get; set; }
        public Penalty Penalty { get; set; }
        public Games Games { get; set; }
        public Substitutes Substitutes { get; set; }
    }
}