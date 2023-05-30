using Solita.Bike.Shared.Dtos;

namespace Solita.Bike.Shared.Responses
{
    public class StationResponse
    {
        public PaginationMetadata Pagination { get; set; }
        public List<StationInfo> Response { get; set; }
    }
}