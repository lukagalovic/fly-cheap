namespace FlyCheap.API.Helpers.Models
{
    public class ObtainTokenRequest
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}
