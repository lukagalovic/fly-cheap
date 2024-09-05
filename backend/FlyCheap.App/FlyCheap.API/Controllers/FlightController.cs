using FlyCheap.API.Data;
using FlyCheap.API.Entities;
using FlyCheap.API.Helpers.Models;
using FlyCheap.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FlyCheap.API.Controllers
{
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFlightSearchResultService _flightSearchResultService;

        public FlightController(
            ILogger<FlightController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration config,
            IFlightSearchResultService flightSearchResultService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = config;
            _flightSearchResultService = flightSearchResultService;
        }

        [HttpGet("flights")]
        public async Task<ActionResult<string>> GetFlights(FlightDestinationRequest req)
        {
            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bearer token is required.");
            }

            var uri = BuildFlightOffersRequestUri(req);
            
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //// pull from database here
            //var existingResults = await _flightSearchResultService.GetFlightSearchResultsAsync(req);

            //if (existingResults.Any())
            //{
            //    return Ok(existingResults);
            //}

            using var client = _httpClientFactory.CreateClient();
            try
            {
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    await SaveSearchToDatabase(responseBody);

                    return Ok(responseBody);
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"FlightController GetFlightDestinations: {response.StatusCode} - {error}");
                    return StatusCode((int)response.StatusCode, error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"FlightController GetFlightDestinations: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        private string BuildFlightOffersRequestUri(FlightDestinationRequest req)
        {
            var baseUrl = _config["Amadeus:BaseFlightOffersUrl"];
            var queryParams = new Dictionary<string, string>
            {
                { "originLocationCode", req.OriginLocationCode },
                { "destinationLocationCode", req.DestinationLocationCode },
                { "departureDate", req.DepartureDate },
                { "adults", req.Adults.ToString() }
            };

            if (req.ReturnDate != null)
                queryParams["returnDate"] = req.ReturnDate;

            if (req.Children.HasValue)
            if (req.Children.HasValue)
                queryParams["children"] = req.Children.Value.ToString();

            if (req.Infants.HasValue)
                queryParams["infants"] = req.Infants.Value.ToString();

            if (!string.IsNullOrEmpty(req.CurrencyCode))
                queryParams["currencyCode"] = req.CurrencyCode;

            if (req.MaxPrice.HasValue)
                queryParams["maxPrice"] = req.MaxPrice.Value.ToString();

            if (req.Max.HasValue)
                queryParams["max"] = req.Max.Value.ToString();

            var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            return $"{baseUrl}?{queryString}";
        }

        private async Task<bool> SaveSearchToDatabase(string responseBody)
        {
            try
            {
                var data = JsonSerializer.Deserialize<ApiResponse>(responseBody);
                var flightResults = MapApiResponseToFlightResults(data);

                await _flightSearchResultService.SaveFlightSearchResultsAsync(flightResults);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"FlightController SaveSearchToDatabase: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return false; 
            }
        }

        private IEnumerable<FlightSearchResult> MapApiResponseToFlightResults(ApiResponse apiResponse)
        {
            return apiResponse.data.Select(flight =>
            {
                var outboundItinerary = flight.itineraries[0]; // Outbound itinerary
                var returnItinerary = flight.itineraries.Count > 1 ? flight.itineraries[1] : null;

                return new FlightSearchResult
                {
                    OriginIata = outboundItinerary.segments[0].departure.iataCode,
                    DestinationIata = outboundItinerary.segments.Last().arrival.iataCode,
                    DepartureDate = outboundItinerary.segments[0].departure.at,
                    ReturnDate = returnItinerary != null ? returnItinerary.segments.Last().arrival.at : (DateTime?)null,
                    OutboundTransfers = outboundItinerary.segments.Count - 1,
                    ReturnTransfers = returnItinerary != null ? returnItinerary.segments.Count - 1 : (int?)null,
                    NumberOfPassengers = flight.travelerPricings.Count,
                    CurrencyCode = flight.price.currency,
                    TotalPrice = decimal.Parse(flight.price.total.Replace('.', ',')),
                    CreatedAt = DateTime.UtcNow
                };
            }).ToList();
        }
    }
}