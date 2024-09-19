namespace uni_cap_pro_be.Core.QueryParameter
{
    // DONE
    public class QueryParameters
    {
        public string? SelectFields { get; set; }
        public string? Filter { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } = "asc"; // asc or desc
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
