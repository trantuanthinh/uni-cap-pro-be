﻿using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO
{
	public class Product_ImageDTO
	{
		public DateTime Created_At { get; set; }

		[Required]
		public required string Name { get; set; }

		[Required]
		public Guid ProductId { get; set; }

	}
}