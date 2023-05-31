using Solita.Bike.Shared.Dtos;
using Solita.Bike.Shared.Responses;

namespace Solita.Bike.App.Data;

public class BikeService
{
    private readonly IHttpClientFactory m_httpClientFactory;

    public BikeService(IHttpClientFactory httpClientFactory) =>
        m_httpClientFactory = httpClientFactory;

    public async Task<JourneyResponse> GetJourneys(int pageNumber = 1, int pageSize = 10)
    {
        var client = m_httpClientFactory.CreateClient("BikeService");
        var response = await client.GetFromJsonAsync<JourneyResponse>($"bike/api/journeys?pageNumber={pageNumber}&PageSize={pageSize}");
        if (response == null)
        {
            throw new Exception("Response was null");
        }

        return response;
    }
    
    public async Task<StationResponse> GetStations(int pageNumber = 1, int pageSize = 10)
    {
        var client = m_httpClientFactory.CreateClient("BikeService");
        var response = await client.GetFromJsonAsync<StationResponse>($"bike/api/stations?pageNumber={pageNumber}&PageSize={pageSize}");
        if (response == null)
        {
            throw new Exception("Response was null");
        }

        return response;
    }
    
    public async Task<SingleStationInfo> GetStation(string id)
    {
        var client = m_httpClientFactory.CreateClient("BikeService");
        var response = await client.GetFromJsonAsync<SingleStationInfo>($"bike/api/stations/{id}");
        if (response == null)
        {
            throw new Exception("Response was null");
        }

        return response;
    }
}
