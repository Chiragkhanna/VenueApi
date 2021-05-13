using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace VenueApi.Models
{
    public class VenueConfigurationCreate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string description { get; set; }
        public int venueId { get; set; }
        public bool active { get; set; }
        public string staticImageUrl { get; set; }
        public string staticImageCompleteUrl { get; set; }
        public bool generalAdmissionOnly { get; set; }
        public int venueConfigurationVersion { get; set; }
        public Map map { get; set; }
        //public Attributes attributes { get; set; }
        public bool blendedIndicator { get; set; }
        public bool seatLevelDataIndicator { get; set; }
        public bool isSectionMapped { get; set; }
        public List<SeatingZone> seatingZones { get; set; }
        public object sectionZoneMetas { get; set; }


    }
    public class SectionZoneMap
    {
        public string c { get; set; }
        public string zi { get; set; }
        public string na { get; set; }
        public string p { get; set; }
        public string t { get; set; }
        public string z { get; set; }
    }
    public class VenueConfigurationRead
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string description { get; set; }
        public int venueId { get; set; }
        public bool active { get; set; }
        public string staticImageUrl { get; set; }
        public string staticImageCompleteUrl { get; set; }
        public bool generalAdmissionOnly { get; set; }
        public int venueConfigurationVersion { get; set; }
        public bool seatLevelDataIndicator { get; set; }
        public bool isSectionMapped { get; set; }
        public List<SeatingZone> seatingZones { get; set; }
        public string sectionZoneMetas { get; set; }

    }
}
