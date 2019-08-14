namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class Season
	{
		public int? Id { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public int? CurrentMatchday { get; set; } 
		public Winner Winner { get; set; }
	}
}