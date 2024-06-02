using System;

namespace entity_17_05.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Studio { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string GameMode { get; set; }
        public int CopiesSold { get; set; }
    }
}