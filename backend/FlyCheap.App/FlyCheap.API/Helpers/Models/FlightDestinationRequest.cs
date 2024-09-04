using System.ComponentModel.DataAnnotations;

namespace FlyCheap.API.Helpers.Models
{
    public class FlightDestinationRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string OriginLocationCode { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string DestinationLocationCode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DepartureDate { get; set; }

        [DataType(DataType.Date)]
        public string ReturnDate { get; set; }

        [Required]
        [Range(1, 9)]
        public int Adults { get; set; } = 1;

        [Range(0, 9)]
        public int? Children { get; set; }

        [Range(0, int.MaxValue)]
        public int? Infants { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Range(0, int.MaxValue)]
        public int? MaxPrice { get; set; }

        [Range(1, 250)]
        public int? Max { get; set; } = 250;
    }
}
