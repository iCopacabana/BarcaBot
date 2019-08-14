namespace Barcabot.Common.DataModels
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public double? Rating { get; set; }
        public Per90Stats Per90Stats { get; set; }
        public Dto.ApiFootball.Goals Goals { get; set; }
    }
}