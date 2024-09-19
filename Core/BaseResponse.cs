namespace uni_cap_pro_be.Core
{
    public class BaseResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public ICollection<T> Data { get; set; }

        public BaseResponse() { }
    }

    // public static class BaseResponseExtensions
    // {
    //     public static BaseResponse<T> GetBaseResponse<T>(
    //         this ICollection<T> values,
    //         int page,
    //         int pageSize,
    //         int totalRecords
    //     )
    //     {
    //         return new BaseResponse<T>
    //         {
    //             Data = values,
    //             Page = page,
    //             PageSize = pageSize,
    //             TotalRecords = totalRecords
    //         };
    //     }
    // }
}
