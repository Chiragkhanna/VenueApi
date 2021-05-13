using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using VenueApi.Interfaces;
using VenueApi.Models;

namespace VenueApi.Services
{
    public class VenueService : IVenueService
    {
        private readonly IMongoCollection<VenueConfiguration> _venueConfigs;

        public VenueService(ITicketDbDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _venueConfigs = database.GetCollection<VenueConfiguration>(settings.VenueConfigurationCollectionName);
        }

        public List<VenueConfiguration> Get() =>
            _venueConfigs.Find(book => true).ToList();

        public VenueConfigurationRead Get(string id) =>
            _venueConfigs.Find<VenueConfiguration>(venue => venue.id == id).ToList().Select(venue => new VenueConfigurationRead()
            {
                id = venue.id,
                active = venue.active,
                description = venue.description,
                generalAdmissionOnly = venue.generalAdmissionOnly,
                isSectionMapped = venue.isSectionMapped,
                seatingZones = venue.seatingZones,
                seatLevelDataIndicator = venue.seatLevelDataIndicator,
                staticImageUrl = venue.staticImageUrl,
                staticImageCompleteUrl = venue.staticImageCompleteUrl,
                venueConfigurationVersion = venue.venueConfigurationVersion,
                venueId = venue.venueId,
                sectionZoneMetas = venue.sectionZoneMetas.ToString()
            }).FirstOrDefault();

        public VenueConfigurationRead GetVenueMap(int venueId) =>
            _venueConfigs.Find<VenueConfiguration>(venue => venue.venueId == venueId).ToList().Select(venue => new VenueConfigurationRead()
            {
                id = venue.id,
                active = venue.active,
                description = venue.description,
                generalAdmissionOnly = venue.generalAdmissionOnly,
                isSectionMapped = venue.isSectionMapped,
                seatingZones = venue.seatingZones,
                staticImageUrl = venue.staticImageUrl,
                staticImageCompleteUrl = venue.staticImageCompleteUrl,
                venueId = venue.venueId,
                sectionZoneMetas = venue.sectionZoneMetas.ToString()
            }).FirstOrDefault();

        public List<SeatingSection> GetSections(int venueId)
        {
            var filter = Builders<VenueConfiguration>.Filter.Eq(p => p.venueId, venueId );
            var projection = Builders<VenueConfiguration>.Projection.Include(p => p.seatingZones);
            var results = _venueConfigs.Find(filter).Project<VenueConfiguration>(projection).FirstOrDefault();
            if(results != null && results.seatingZones.Count > 0)
            {
                List<SeatingSection> seatingSections = new List<SeatingSection>();
                foreach (var item in results.seatingZones)
                {
                    seatingSections.AddRange(item.seatingSections);
                }
                return seatingSections;
            }
            return null; 
        }

        public VenueConfiguration Create(VenueConfigurationCreate venue)
        {
            VenueConfiguration newVenue = new VenueConfiguration()
            {
                id = ObjectId.GenerateNewId().ToString(),
                active = venue.active,
                blendedIndicator = venue.blendedIndicator,
                description = venue.description,
                generalAdmissionOnly = venue.generalAdmissionOnly,
                isSectionMapped = venue.isSectionMapped,
                map = venue.map,
                seatingZones = venue.seatingZones,
                seatLevelDataIndicator = venue.seatLevelDataIndicator,
                staticImageUrl = venue.staticImageUrl,
                staticImageCompleteUrl = venue.staticImageCompleteUrl,
                venueConfigurationVersion = venue.venueConfigurationVersion,
                venueId = venue.venueId,
                sectionZoneMetas = BsonDocument.Parse(venue.sectionZoneMetas.ToString())
            };
            _venueConfigs.InsertOne(newVenue);
            return newVenue;
        }

        public void Update(string id, VenueConfiguration venueIn) =>
            _venueConfigs.ReplaceOne(venue => venue.id == id, venueIn);

        public void Remove(VenueConfiguration bookIn) =>
            _venueConfigs.DeleteOne(venue => venue.id == bookIn.id);

        public void Remove(string id) =>
            _venueConfigs.DeleteOne(venue => venue.id == id);
        public void RemoveByVenueId(int id) =>
           _venueConfigs.DeleteMany(venue => venue.venueId == id);
    }
}
