namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class Score
	{
		public string Winner { get; set; }
		public string Duration { get; set; }
		public Result FullTime { get; set; }
		public Result HalfTime { get; set; }
		public Result ExtraTime { get; set; }
		public Result Penalties { get; set; }
	}
}