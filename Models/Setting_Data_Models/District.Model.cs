using CsvHelper.Configuration.Attributes;
using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models.Setting_Data_Models
{
    // DONE
    public class District : BaseEntity<string>
    {
        public string Name { get; set; }
        public string ProvinceId { get; set; }

        public Province? Province { get; set; }
        public ICollection<Ward>? Wards { get; set; }
    }
}
