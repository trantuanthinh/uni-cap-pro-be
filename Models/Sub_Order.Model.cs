﻿using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
	public class Sub_Order
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required Guid UserId { get; set; }
		public Guid OrderId { get; set; }

		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}