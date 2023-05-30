using Solita.Bike.Shared.Dtos;

namespace Solita.Bike.Shared.Responses
{
    public class JourneyResponse
    {
        public PaginationMetadata Pagination { get; set; }
        public List<JourneyInfo> Response { get; set; }
    }
}