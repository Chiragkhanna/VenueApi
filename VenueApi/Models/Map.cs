namespace VenueApi.Models
{
    public class Map
    {
        public int mapFormatId { get; set; }
        public int mapType { get; set; }
        public bool rowOverlaySwitch { get; set; }
        public bool viewFromSection { get; set; }
        public bool virtualRealityEnabled { get; set; }
        public bool sectionScrubbing { get; set; }
        public bool rowScrubbing { get; set; }
    }
}
