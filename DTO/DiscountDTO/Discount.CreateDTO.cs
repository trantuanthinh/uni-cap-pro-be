﻿using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.DiscountDTO
{
	public class DiscountCreateDTO
	{
		[Required]
		public required short Level { get; set; }

		[Required]
		public required double Amount { get; set; }
	}
}