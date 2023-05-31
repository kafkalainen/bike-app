using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CsvHelper;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Solita.Bike.Database;
using Solita.Bike.Models;

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


await ImportStations();
await ImportJourneys();

Console.WriteLine("All data has been successfully imported.");

async Task ImportStations()
{
    foreach (var file in Directory.GetFiles("Data/Stations"))
    {
        await using var dbContext = new BikeDbContext();
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

async Task ImportJourneys()
{
    foreach (var file in Directory.GetFiles("Data/Journeys"))
    {
        Console.WriteLine($"Importing: {file}");
        await using var dbContext = new BikeDbContext();
        if (dbContext.DataImports.Any(di => di.FileName == file))
        {
            Console.WriteLine($"Data from {file} has already been imported. Skipping.");
            continue;
        }
    
        using var reader = new StreamReader(file);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<JourneyRecord>().ToList();
        
        var batchSize = 100000;
        var journeys = new List<Journey>();
        var importedCount = 0;
        var startTime = DateTime.UtcNow;

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

            var departureIsInStations = await ValidateForeignKeyAsync(dbContext, journey.DepartureStationId!);
            var returnIsInStations = await ValidateForeignKeyAsync(dbContext, journey.ReturnStationId!);
            if (!departureIsInStations || !returnIsInStations)
            {
                continue;
            }
            
            journeys.Add(journey);
            importedCount++;

            if (journeys.Count < batchSize && importedCount != records.Count)
            {
                continue;
            }
            
            await using var innerDbContext = new BikeDbContext();
            await innerDbContext.Journeys.AddRangeAsync(journeys);
            try
            {
                await innerDbContext.BulkSaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                foreach (var entry in e.Entries)
                {
                    Console.WriteLine($"Entity Type: {entry.Entity.GetType().Name}");
                    foreach (var property in entry.Properties)
                    {
                        Console.WriteLine($"Property: {property.Metadata.Name}, Value: {property.CurrentValue}");
                    }
                }
            }
            finally
            {
                journeys.Clear();
                var elapsedSeconds = (DateTime.UtcNow - startTime).TotalSeconds;
                var averageTimePerRecord = elapsedSeconds / importedCount;
                var remainingCount = records.Count - importedCount;
                var estimatedTimeLeft = TimeSpan.FromSeconds(averageTimePerRecord * remainingCount);
                Console.WriteLine($"Batch imported. Total imported: {importedCount/(double)records.Count * 100}%");
                Console.WriteLine($"Estimated time left: {estimatedTimeLeft}");
            }
        }
        await dbContext.DataImports.AddAsync(new DataImport { FileName = file, ImportDate = DateTime.UtcNow });
        await dbContext.SaveChangesAsync();
        Console.WriteLine($"Data from {file} has been imported.");
    }
}

async Task<bool> ValidateForeignKeyAsync(BikeDbContext dbContext, string stationId)
{
    var result = await dbContext.Stations.AnyAsync(s => s.Id == stationId);
    return result;
}