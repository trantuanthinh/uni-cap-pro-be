using CsvHelper.Configuration.Attributes;
using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models.Setting_Data_Models
{
    // DONE
    public class Province : BaseEntity<string>
    {
        public string Name { get; set; }

        public ICollection<District>? Districts { get; set; }
    }
}
