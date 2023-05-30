using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Solita.Bike.Api;
using Solita.Bike.Shared;
using Solita.Bike.Shared.Dtos;
using Solita.Bike.Shared.Models;
using Solita.Bike.Shared.Profiles;
using Solita.Bike.Shared.Responses;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BikeDbContext>();
builder.Services.AddAutoMapper(typeof(JourneyMappingProfile).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var mapper = app.Services.GetService<IMapper>();
if (mapper == null)
{
    throw new InvalidOperationException($"Couldn't resolve service {nameof(IMapper)}");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("bike/api/journeys", async ([AsParameters]QueryParameters parameters, BikeDbContext db) =>
{
    var pagedResult = await PaginatedList<Journey>.CreateAsync(db.Journeys, parameters.PageNumber, parameters.PageSize);
    var list = mapper.Map<PaginatedList<JourneyInfo>>(pagedResult);
    var paginationMetadata = new PaginationMetadata
    {
        PageNumber = list.PageIndex,
        PageSize = list.PageSize,
        TotalPages = list.TotalPages,
        HasPreviousPage = list.HasPreviousPage,
        HasNextPage = list.HasNextPage
    };
    return Results.Ok( new JourneyResponse(){ Pagination = paginationMetadata, Response = list });
}).WithName("Journeys").WithOpenApi();

app.MapGet("bike/api/stations", async ([AsParameters] QueryParameters parameters, BikeDbContext db) =>
{
    var pagedResult = await PaginatedList<Station>.CreateAsync(db.Stations, parameters.PageNumber, parameters.PageSize);
    var list = mapper.Map<List<StationInfo>>(pagedResult);
    var paginationMetadata = new PaginationMetadata
    {
        PageNumber = pagedResult.PageIndex,
        PageSize = pagedResult.PageSize,
        TotalPages = pagedResult.TotalPages,
        HasPreviousPage = pagedResult.HasPreviousPage,
        HasNextPage = pagedResult.HasNextPage
    };
    return Results.Ok(new StationResponse { Pagination = paginationMetadata, Response = list });
}).WithName("Stations").WithOpenApi();

app.MapGet("bike/api/stations/{id}", async (string id, BikeDbContext db) =>
{
    var result = await db.Stations
        .Where(s => s.Id == id)
        .Select(s => new SingleStationInfo
        {
            Name = new Dictionary<Localization, string?>
            {
                { Localization.Fi, s.NameInFinnish },
                { Localization.Sv, s.NameInSwedish },
                { Localization.En, s.NameInEnglish }
            },
            Address = new Dictionary<Localization, string?>
            {
                { Localization.Fi, s.AddressInFinnish },
                { Localization.Sv, s.AddressInSwedish }
            },
            StartJourneyTotal = (ulong)s.DepartureJourneys.Count(),
            EndJourneyTotal = (ulong)s.ReturnJourneys.Count()
        })
        .FirstOrDefaultAsync();
    return mapper.Map<SingleStationInfo>(result);
}).WithName("StationWithId").WithOpenApi();

app.Run();