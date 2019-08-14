using System.Collections.Generic;

namespace Barcabot.Common.DataModels.Dto.FootballDataApi.MatchesOnly
{
	public class Filters
	{
		public string Permission { get; set; }
		public List<string> Status { get; set; }
		public int Limit { get; set; }
	}
}