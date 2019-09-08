namespace Barcabot.Common.DataModels
{
    public class StandingsTeam
    {
        public int Position { get; set; }
        public string Team { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int Gd { get; set; }
        public int Points { get; set; }
    }
}