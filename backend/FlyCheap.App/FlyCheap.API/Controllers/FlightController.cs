using FlyCheap.API.Data;
using FlyCheap.API.Entities;
using FlyCheap.API.Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FlyCheap.API.Controllers
{
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public FlightController(ILogger<FlightController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("flights")]
        public async Task<ActionResult<string>> GetFlights(FlightDestinationRequest req)
        {
            //if (string.IsNullOrEmpty(req.) || maxPrice <= 0)
            //{
            //    return BadRequest("Origin and MaxPrice parameters are required.");
            //}

            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bearer token is required.");
            }

            var url = BuildFlightsUrl(req);
            using var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
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
                _logger.LogError($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        private string BuildFlightsUrl(FlightDestinationRequest req)
        {
            var baseUrl = "https://test.api.amadeus.com/v2/shopping/flight-offers";
            var query = new Dictionary<string, string>
            {
                { "originLocationCode", req.OriginLocationCode },
                { "destinationLocationCode", req.DestinationLocationCode },
                { "departureDate", req.DepartureDate },
                { "adults", req.Adults.ToString() }
            };

            if (req.ReturnDate != null)
                query["returnDate"] = req.ReturnDate;

            if (req.Children.HasValue)
                query["children"] = req.Children.Value.ToString();

            if (req.Infants.HasValue)
                query["infants"] = req.Infants.Value.ToString();

            if (!string.IsNullOrEmpty(req.CurrencyCode))
                query["currencyCode"] = req.CurrencyCode;

            if (req.MaxPrice.HasValue)
                query["maxPrice"] = req.MaxPrice.Value.ToString();

            if (req.Max.HasValue)
                query["max"] = req.Max.Value.ToString();

            var queryString = string.Join("&", query.Select(kv => $"{WebUtility.UrlEncode(kv.Key)}={WebUtility.UrlEncode(kv.Value)}"));
            return $"{baseUrl}?{queryString}";
        }
    }
}