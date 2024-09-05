using System.ComponentModel.DataAnnotations;

namespace FlyCheap.API.Entities
{
    public class FlightSearchResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string OriginIata { get; set; }

        [Required]
        [StringLength(3)]
        public string DestinationIata { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int OutboundTransfers { get; set; }
        public int? ReturnTransfers { get; set; }
        public int NumberOfPassengers { get; set; }
        public string CurrencyCode { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
