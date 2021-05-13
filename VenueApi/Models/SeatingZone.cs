using System.Collections.Generic;
namespace VenueApi.Models
{
    public class SeatingZone
    {
        public int id { get; set; }
        public string name { get; set; }
        public int displaySortOrder { get; set; }
        public List<SeatingSection> seatingSections { get; set; }
    }
}
