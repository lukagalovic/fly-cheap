using FlyCheap.API.Data;
using FlyCheap.API.Entities;
using FlyCheap.API.Helpers.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyCheap.API.Services
{
    public class FlightSearchResultService : IFlightSearchResultService
    {
        private readonly FlyCheapDbContext _ctx;

        public FlightSearchResultService(FlyCheapDbContext context)
        {
            _ctx = context;
        }

        public async Task SaveFlightSearchResultsAsync(IEnumerable<FlightSearchResult> results)
        {
            await _ctx.FlightSearchResults.AddRangeAsync(results);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<FlightSearchResult>> GetFlightSearchResultsAsync(FlightDestinationRequest req)
        {
            //return await _ctx.FlightSearchResults
            //    .Where(f => f.OriginIata == req.OriginLocationCode &&
            //                f.DestinationIata == req.DestinationLocationCode &&
            //                f.DepartureDate == DateTime.Parse(req.DepartureDate) &&
            //                f.NumberOfPassengers == req.Adults &&
            //                f.CurrencyCode == req.CurrencyCode)
            //    .ToListAsync();
            return await _ctx.FlightSearchResults
                .Where(f => f.OriginIata.Trim().Equals(req.OriginLocationCode.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
                            f.DestinationIata.Trim().Equals(req.DestinationLocationCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }
    }
}
