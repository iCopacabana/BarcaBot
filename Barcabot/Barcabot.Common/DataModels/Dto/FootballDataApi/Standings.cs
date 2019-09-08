using System.Collections.Generic;

namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
    public class Standings
    {
        public string Stage { get; set; }
        public string Type { get; set; }
        public List<StandingsTeam> Table { get; set; }
    }
}