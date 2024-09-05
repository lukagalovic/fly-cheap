using CsvHelper;
using CsvHelper.Configuration;
using FlyCheap.API.Data;
using FlyCheap.API.Entities;
using FlyCheap.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace FlyCheap.API.Helpers
{
    public sealed class DbInitializer : IDbInitializer
    {
        private readonly FlyCheapDbContext _ctx;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(FlyCheapDbContext ctx, ILogger<DbInitializer> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task SeedDatabaseAsync(string filePath)
        {
            try
            {
                if (!_ctx.Airports.Any())
                {
                    if (!File.Exists(filePath))
                    {
                        _logger.LogError($"File containing information about airports does not exist. Path: {filePath}");
                        throw new FileNotFoundException(filePath);
                    }

                    using var reader = new StreamReader(filePath);
                    using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                    
                    csv.Context.RegisterClassMap<AirportMap>();

                    var records = csv.GetRecords<Airport>().ToList();
                    _ctx.Airports.AddRange(records);
                    await _ctx.SaveChangesAsync();
                }
                else
                {
                    _logger.LogInformation("Nothing to seed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error seeding database: {ex.Message}");
            }
        }
    }
}
