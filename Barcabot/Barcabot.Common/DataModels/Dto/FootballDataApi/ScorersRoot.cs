using System.Collections.Generic;
using Barcabot.Common.DataModels.Dto.FootballDataApi.ScorersOnly;

namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class ScorersRoot
	{
		public int Count { get; set; }
		public Filters Filters { get; set; }
		public Competition Competition { get; set; }
		public Season Season { get; set; }
		public List<Scorer> Scorers { get; set; }
	}
}