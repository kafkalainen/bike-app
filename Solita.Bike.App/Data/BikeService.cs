using Solita.Bike.Shared;
using Solita.Bike.Shared.Dtos;
using Solita.Bike.Shared.Responses;

namespace Solita.Bike.App.Data;

public class BikeService
{
    private readonly HttpClient m_httpClient;

    public List<JourneyInfo>? JourneyInfos { get; set; }
    public PaginationMetadata? Metadata;

    public BikeService(HttpClient httpClient)
    {
        m_httpClient = httpClient;
    }

    public async Task<JourneyResponse> GetJourneys(int pageNumber = 1, int pageSize = 10)
    {
        var response = await m_httpClient.GetFromJsonAsync<JourneyResponse>($"http://localhost:5783/bike/api/journeys?pageNumber={pageNumber}&PageSize={pageSize}");
        if (response == null)
        {
            throw new Exception("Response was null");
        }

        return response;
    }
    
    public async Task<StationResponse> GetStations(int pageNumber = 1, int pageSize = 10)
    {
        var response = await m_httpClient.GetFromJsonAsync<StationResponse>($"http://localhost:5783/bike/api/stations?pageNumber={pageNumber}&PageSize={pageSize}");
        if (response == null)
        {
            throw new Exception("Response was null");
        }

        return response;
    }
}
