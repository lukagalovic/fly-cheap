using FlyCheap.API.Entities;
using FlyCheap.API.Helpers.Models;

namespace FlyCheap.API.Services
{
    public interface IFlightSearchResultService
    {
        public Task SaveFlightSearchResultsAsync(IEnumerable<FlightSearchResult> results);
        public Task<IEnumerable<FlightSearchResult>> GetFlightSearchResultsAsync(FlightDestinationRequest req);
    }
}
