﻿using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO
{
    public class OrderDTO
    {
        public Guid OwnerId { get; set; }

        [Required]
        public required double Total_Price { get; set; }

        [Required]
        public DeliveryStatus Delivery_Status { get; set; }

        public required User Owner { get; set; }
    }
}