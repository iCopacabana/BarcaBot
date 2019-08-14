namespace Barcabot.Common.DataModels.Dto.FootballDataApi
{
	public class Player
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string DateOfBirth { get; set; }
		public string CountryOfBirth { get; set; }
		public string Nationality { get; set; }
		public string Position { get; set; }
		public int? ShirtNumber { get; set; }
		public string LastUpdated { get; set; }
	}
}