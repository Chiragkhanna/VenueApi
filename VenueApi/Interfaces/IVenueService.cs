using System.Collections.Generic;
using VenueApi.Models;

namespace VenueApi.Interfaces
{
    public interface IVenueService
    {
        List<VenueConfiguration> Get();
        VenueConfigurationRead Get(string id);
        VenueConfiguration Create(VenueConfigurationCreate venue);

        void Update(string id, VenueConfiguration venueIn);
        void Remove(VenueConfiguration bookIn);
        void Remove(string id);
        void RemoveByVenueId(int id);
        List<SeatingSection> GetSections(int venueId);
        VenueConfigurationRead GetVenueMap(int venueId);
    }
}
