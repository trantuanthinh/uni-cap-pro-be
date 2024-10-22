using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models.Setting_Data_Models
{
    // DONE
    public class UnitMeasure : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
    }
}
