namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class Competition
	{
		public int Id { get; set; }
		public Entity Area { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Plan { get; set; }
		public string LastUpdated { get; set; }
	}
}
