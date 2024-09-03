using CsvHelper.Configuration;
using FlyCheap.API.Entities;

namespace FlyCheap.API.Helpers
{
    public class AirportMap : ClassMap<Airport>
    {
        public AirportMap()
        {
            Map(m => m.Iata).Name("code");
            Map(m => m.Icao).Name("icao");
            Map(m => m.Name).Name("name");
            Map(m => m.Latitude).Name("latitude");
            Map(m => m.Longitude).Name("longitude");
            Map(m => m.Elevation).Name("elevation");
            Map(m => m.Url).Name("url");
            Map(m => m.TimeZone).Name("time_zone");
            Map(m => m.CityCode).Name("city_code");
            Map(m => m.Country).Name("country");
            Map(m => m.City).Name("city");
            Map(m => m.State).Name("state");
            Map(m => m.County).Name("county");
            Map(m => m.Type).Name("type");
        }
    }
}
