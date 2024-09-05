using FlyCheap.API.Helpers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlyCheap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(
            ILogger<AuthController> logger,
            IConfiguration config,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        
        [HttpPost(Name = "GetAccessToken")]
        public async Task<ActionResult<string>> GetToken([FromBody] ObtainTokenRequest requestModel)
        {
            if (requestModel == null || string.IsNullOrEmpty(requestModel.ClientId) || string.IsNullOrEmpty(requestModel.ClientSecret))
            {
                return BadRequest("ClientId and ClientSecret are required.");
            }

            using var client = _httpClientFactory.CreateClient();
            string uri = _config["Amadeus:AuthTokenUrl"]!;

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", requestModel.ClientId),
                    new KeyValuePair<string, string>("client_secret", requestModel.ClientSecret)
            ]);
            request.Content = content;

            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode
                    ? Ok(result)
                    : StatusCode((int)response.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error: {0}\nStackTrace: {1}", ex.Message, ex.StackTrace));
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
