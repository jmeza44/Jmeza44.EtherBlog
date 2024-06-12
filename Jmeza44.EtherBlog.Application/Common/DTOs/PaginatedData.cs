namespace Jmeza44.EtherBlog.Application.Common.DTOs
{
    public class PaginatedData<T>
    {
        public int TotalCount { get; set; }
        public required List<T> Data { get; set; }
    }
}
