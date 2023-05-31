using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Solita.Bike.Database;
using Solita.Bike.Shared;
using Solita.Bike.Shared.Models;

var downloader = new FileDownloader();
var downloadTasks = new List<Task>
{
    downloader.DownloadFile("https://opendata.arcgis.com/datasets/726277c507ef4914b0aec3cbcfcbfafc_0.csv",
        "Data/Stations/Espoon_ja_Helsingin_Kaupunkipyoraasemat_avoin.csv"),
    downloader.DownloadFile("https://dev.hsl.fi/citybikes/od-trips-2021/2021-05.csv",
        "Data/Journeys/2021-05.csv"),
    downloader.DownloadFile("https://dev.hsl.fi/citybikes/od-trips-2021/2021-06.csv",
        "Data/Journeys/2021-06.csv"),
    downloader.DownloadFile("https://dev.hsl.fi/citybikes/od-trips-2021/2021-07.csv",
        "Data/Journeys/2021-07.csv")
};


await Task.WhenAll(downloadTasks);

await using var context = new BikeDbContext();
await ImportStations(context);
await ImportJourneys(context);

async Task ImportStations(BikeDbContext dbContext)
{
    foreach (var file in Directory.GetFiles("Data/Stations"))
    {
        if (dbContext.DataImports.Any(di => di.FileName == file))
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
            await dbContext.Stations.AddAsync(station);
        }

        await dbContext.DataImports.AddAsync(new DataImport { FileName = file, ImportDate = DateTime.UtcNow });
        await dbContext.SaveChangesAsync();
        Console.WriteLine($"Data from {file} has been imported.");
    }
}

async Task ImportJourneys(BikeDbContext dbContext)
{
    foreach (var file in Directory.GetFiles("Data/Journeys"))
    {
        if (dbContext.DataImports.Any(di => di.FileName == file))
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
    
            if (!Validator.TryValidateObject(journey, validationContext, validationResults, true))
            {
                continue;
            }
            
            await dbContext.Journeys.AddAsync(journey);
        }
    
        await dbContext.DataImports.AddAsync(new DataImport { FileName = file, ImportDate = DateTime.UtcNow });
        try
        {
            dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            foreach (var entry in e.Entries)
            {
                Console.WriteLine($"Entity Type: {entry.Entity.GetType().Name}");
                foreach (var property in entry.Properties)
                {
                    Console.WriteLine($"Property: {property.Metadata.Name}, Value: {property.CurrentValue}");
                }
            }
        }
        Console.WriteLine($"Data from {file} has been imported.");
    }
}

Console.WriteLine("All data has been successfully imported.");