namespace Solita.Bike.Api
{
    public class QueryParameters
    {
        private const int MaxPageSize = 2000;
        public int PageNumber { get; set; } = 1;
        private int m_pageSize = 10;
        public int PageSize
        {
            get => m_pageSize;
            set => m_pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}