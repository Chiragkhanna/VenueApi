using VenueApi.Interfaces;

namespace VenueApi.Models
{
    public class TicketDbDatabaseSettings : ITicketDbDatabaseSettings
    {
        public string VenueConfigurationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

  
}
