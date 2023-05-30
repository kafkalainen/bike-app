using Solita.Bike.Shared.Dtos;

namespace Solita.Bike.Shared;

public class JourneyResponse
{
    public PaginationMetadata Pagination { get; set; }
    public PaginatedList<JourneyInfo> Response { get; set; }
}