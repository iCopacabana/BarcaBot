namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class Head2Head
	{
		public string NumberOfMatches { get; set; }
		public int TotalGoals { get; set; }
		public H2HResults HomeTeam { get; set; }
		public H2HResults AwayTeam { get; set; }
	}
}