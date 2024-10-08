﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Order : BaseEntity<Guid>
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductResponse>();
        });

        static readonly Mapper mapper = new Mapper(config);

        public ProductResponse ToResponse()
        {
            var res = mapper.Map<ProductResponse>(this);
            return res;
        }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required Guid ProductId { get; set; }

        [Required]
        public required double Total_Price { get; set; }

        [Required]
        public required int Total_Quantity { get; set; }

        public DateTime EndTime { get; set; }
        public DeliveryStatus Delivery_Status { get; set; }
        public int Level { get; set; } //number of people joined together

        [Required]
        public required bool IsShare { get; set; }

        [Required]
        public required bool IsPaid { get; set; }

        public Product? Product { get; set; }
        public List<Sub_Order>? Sub_Orders { get; set; }
    }
}
