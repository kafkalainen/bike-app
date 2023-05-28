using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Solita.Bike.Database.Data;
using Solita.Bike.Shared;
using Solita.Bike.Shared.Models;

await using var context = new BikeDbContext();

foreach (var file in Directory.GetFiles("Data/Stations"))
{
    if (context.DataImports.Any(di => di.FileName == file))
    {
        Console.WriteLine($"Data from {file} has already been imported. Skipping.");
        continue;
    }

    using var reader = new StreamReader(file);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
    var records = csv.GetRecords<StationRecord>();

    foreach (var record in records)
    {
        var station = new Station
        {
            Fid = record.Fid,
            Id = record.Id,
            NameInFinnish = record.NameInFinnish,
            NameInEnglish = record.NameInEnglish,
            NameInSwedish = record.NameInSwedish,
            AddressInFinnish = record.AddressInFinnish,
            AddressInSwedish = record.AddressInSwedish,
            CityInFinnish = record.CityInFinnish,
            CityInSwedish = record.CityInSwedish,
            Operator = record.Operator,
            Capacity = record.Capacity,
            X = record.X,
            Y = record.Y
        };
        await context.Stations.AddAsync(station);
    }

    await context.DataImports.AddAsync(new DataImport { FileName = file, ImportDate = DateTime.UtcNow });
    await context.SaveChangesAsync();
}

foreach (var file in Directory.GetFiles("Data/Journeys"))
{
    if (context.DataImports.Any(di => di.FileName == file))
    {
        Console.WriteLine($"Data from {file} has already been imported. Skipping.");
        continue;
    }

    using var reader = new StreamReader(file);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
    var records = csv.GetRecords<JourneyRecord>();
    
    foreach (var record in records)
    {
        var journey = new Journey
        {
            Departure = record.Departure,
            Return = record.Return,
            DepartureStationId = record.DepartureStationId,
            DepartureStationName = record.DepartureStationName,
            ReturnStationId = record.ReturnStationId,
            ReturnStationName = record.ReturnStationName,
            CoveredDistanceInMeters = record.CoveredDistance,
            DurationInSeconds = record.Duration
        };
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(journey);

        var exists = await context.Stations.AnyAsync(s => record.DepartureStationId == s.Id);
        if (!exists)
        {
            continue;
        }
        
        exists = await context.Stations.AnyAsync(s => record.ReturnStationId == s.Id);
        if (!exists)
        {
            continue;
        }
        
        if (!Validator.TryValidateObject(journey, validationContext, validationResults, true))
        {
            continue;
        }
        
        await context.Journeys.AddAsync(journey);
    }

    await context.DataImports.AddAsync(new DataImport { FileName = file, ImportDate = DateTime.UtcNow });
    await context.SaveChangesAsync();
}

Console.WriteLine("All data has been successfully imported.");