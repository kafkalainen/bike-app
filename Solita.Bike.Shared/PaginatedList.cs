using Microsoft.EntityFrameworkCore;

namespace Solita.Bike.Shared
{
    public class PaginatedList<T> : List<T>
    {
        private const int MaxPageSize = 2000;
        public int PageIndex { get; set; }
        
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
    
        public bool HasNextPage => PageIndex < TotalPages;
        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
