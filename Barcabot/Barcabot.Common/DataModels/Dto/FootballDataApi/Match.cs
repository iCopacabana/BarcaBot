using System.Collections.Generic;

namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class Match
	{
		public int Id { get; set; }
		public Entity Competition { get; set; }
		public Season Season { get; set; }
		public string UtcDate { get; set; }
		public string Status { get; set; }
		public string Venue { get; set; }
		public int? Matchday { get; set; }
		public string Stage { get; set; }
		public string Group { get; set; }
		public string LastUpdated { get; set; }
		public Score Score { get; set; }
		public Entity HomeTeam { get; set; }
		public Entity AwayTeam { get; set; }
		public List<string> Referees { get; set; }
	}
}