using System.Text.Json.Serialization;
using uni_cap_pro_be.Core.Base.Entity;

namespace uni_cap_pro_be.Extensions
{
    public static class PatchExtensions
    {
        public static void Patch<T, D>(this PatchRequest<T> src, ref D dest)
            where T : class
            where D : class
        {
            // Set Modified_At to now if it exists
            var modifiedAtProperty = dest.GetType().GetProperty("Modified_At");
            if (modifiedAtProperty != null && modifiedAtProperty.CanWrite)
            {
                modifiedAtProperty.SetValue(dest, DateTime.UtcNow);
            }

            foreach (var field in src.Fields)
            {
                var srcProp = typeof(T).GetProperty(CapitalizeFirstLetter(field));

                var destProp = typeof(D).GetProperty(CapitalizeFirstLetter(field));

                if (destProp != null && destProp.CanWrite)
                {
                    var srcValue = srcProp?.GetValue(src);

                    if (
                        srcValue != null
                        && !srcProp.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)
                        && !srcProp.Name.Equals("Created_At", StringComparison.OrdinalIgnoreCase)
                        && !srcProp.Name.Equals("Modified_At", StringComparison.OrdinalIgnoreCase)
                        && !srcProp.GetCustomAttributes(false).OfType<JsonIgnoreAttribute>().Any()
                    )
                    {
                        destProp.SetValue(dest, srcValue);
                    }
                }
            }
        }

        private static string CapitalizeFirstLetter(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
