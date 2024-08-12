using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO
{
	public class Product_CategoryDTO
	{
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required string Name { get; set; }
	}
}
