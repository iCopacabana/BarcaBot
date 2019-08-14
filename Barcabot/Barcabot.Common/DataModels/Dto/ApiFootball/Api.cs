using System.Collections.Generic;

namespace Barcabot.Common.DataModels.Dto.ApiFootball
{
    public class Api
    {
        public int Results { get; set; }
        public List<FootballPlayer> Players { get; set; }
    }
}