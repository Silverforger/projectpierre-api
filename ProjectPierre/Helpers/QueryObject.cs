namespace ProjectPierre.Helpers
{
    public class QueryObject
    {
        public string? Filter { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
