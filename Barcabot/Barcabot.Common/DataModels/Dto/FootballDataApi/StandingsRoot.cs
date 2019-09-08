using System.Collections.Generic;

namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
    public class StandingsRoot
    {
        public object Filters { get; set; }
        public Competition Competition { get; set; }
        public Season Season { get; set; }
        public List<Standings> Standings { get; set; }
    }
}