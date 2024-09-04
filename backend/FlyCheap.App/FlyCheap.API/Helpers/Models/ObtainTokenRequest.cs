using System.ComponentModel.DataAnnotations;

namespace FlyCheap.API.Helpers.Models
{
    public class ObtainTokenRequest
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
}
