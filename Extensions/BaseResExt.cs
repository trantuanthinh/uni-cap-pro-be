using uni_cap_pro_be.Core;

namespace uni_cap_pro_be.Extensions
{
    public static class BaseResponseExtensions
    {
        public static BaseResponse<T> GetBaseResponse<T>(
            this ICollection<T> values,
            int page,
            int pageSize,
            int totalRecords
        )
            where T : class
        {
            return new BaseResponse<T>
            {
                Data = values,
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}
