using uni_cap_pro_be.Core.Data.Base.Entity;
using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
    // DONE
	public class Product_Image : BaseEntity<Guid>
	{
		public DateTime Created_At { get; set; }

		[Required]
		public required string Name { get; set; }

		[Required]
		public Guid ProductId { get; set; }



		[Required]
		public required Product Product { get; set; }
	}
}
