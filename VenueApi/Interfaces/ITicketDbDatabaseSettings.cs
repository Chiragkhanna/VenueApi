namespace VenueApi.Interfaces
{
    public interface ITicketDbDatabaseSettings
    {
        string VenueConfigurationCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
