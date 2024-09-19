using System.ComponentModel.DataAnnotations.Schema;

namespace uni_cap_pro_be.Core.Data.Base.Entity
{
    // DONE
    public class BaseEntity<IdType>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required IdType Id { get; set; }
    }
}
