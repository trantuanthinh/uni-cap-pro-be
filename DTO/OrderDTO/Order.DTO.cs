﻿using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.OrderDTO
{
	public class OrderDTO
	{
		public Guid Id { get; set; }
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		public required double Total_Price { get; set; }

		public required int Total_Quantity { get; set; }
		public DateTime EndTime { get; set; }

		public TimeSpan TimeLeft { get; set; }
		public bool Is_Remained { get; set; }
		public DeliveryStatus Delivery_Status { get; set; }

		public int Level { get; set; } //number of people joined together
		public bool IsShare { get; set; }
		public List<Sub_Order>? Sub_Orders { get; set; }
		public Product? Product { get; set; }
	}
}