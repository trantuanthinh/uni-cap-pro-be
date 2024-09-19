namespace uni_cap_pro_be.Core.QueryParameter
{
    public class QueryParameterResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public IQueryable<T> Data { get; set; }

        public QueryParameterResult(IQueryable<T> data, int page, int pageSize, int totalRecords)
        {
            Page = page;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            Data = data;
        }
    }
}
