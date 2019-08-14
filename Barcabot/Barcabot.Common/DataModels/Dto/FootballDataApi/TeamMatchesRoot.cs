using System.Collections.Generic;
using Barcabot.Common.DataModels.Dto.FootballDataApi.MatchesOnly;

namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class TeamMatchesRoot
	{
		public int Count { get; set; }
		public Filters Filters { get; set; }
		public List<Match> Matches { get; set; }
	}
}