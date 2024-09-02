﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("destinations")]
        public async Task<ActionResult<string>> GetFlightDestinations(string origin, decimal maxPrice)
        {
            if (string.IsNullOrEmpty(origin) || maxPrice <= 0)
            {
                return BadRequest("Origin and MaxPrice parameters are required.");
            }

            var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Bearer token is required.");
            }

            var flightDestinationsUrl = $"https://test.api.amadeus.com/v1/shopping/flight-destinations?origin={origin}&maxPrice={maxPrice}";

            using (var client = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, flightDestinationsUrl);
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response;
                try
                {
                    response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        return Ok(responseBody);
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        _logger.LogError($"API Error: {response.StatusCode} - {errorContent}");
                        return StatusCode((int)response.StatusCode, errorContent);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    return StatusCode(500, "Internal server error");
                }
            }
        }
    }
}