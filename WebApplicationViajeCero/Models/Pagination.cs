namespace WebApplicationViajeCero.Models
{
    public class Pagination
    {
        public class PagedResult<T>
        {
            public int Total { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
            public IEnumerable<T> Data { get; set; }
        }

    }
}
