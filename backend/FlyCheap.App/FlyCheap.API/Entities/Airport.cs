using System.ComponentModel.DataAnnotations;

namespace FlyCheap.API.Entities
{
    public sealed class Airport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Iata { get; set; }

        [StringLength(10)]
        public string Icao { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? Elevation { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        [StringLength(50)]
        public string TimeZone { get; set; }

        [StringLength(10)]
        public string CityCode { get; set; }

        [StringLength(2)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(100)]
        public string County { get; set; }

        [StringLength(2)]
        public string Type { get; set; }
    }
}
