namespace uni_cap_pro_be.Core
{
    // DONE
    public class BaseResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public ICollection<T> Data { get; set; }

        public BaseResponse() { }
    }
}
