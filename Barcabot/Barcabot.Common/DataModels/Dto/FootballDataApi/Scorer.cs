namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class Scorer
	{
		public Player Player { get; set; }
		public Entity Team { get; set; }
		public int? NumberOfGoals { get; set; }
	}
}