using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Item_Order : BaseEntity<Guid>
    {
        // Mapping from Item_Order to Item_OrderResponse
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Item_Order, Item_OrderResponse>()
                .ForMember(d => d.Product, opt => opt.MapFrom(src => src.Product.ToResponse()));
        });

        static readonly IMapper mapper = new Mapper(config);

        public Item_OrderResponse ToResponse()
        {
            var res = mapper.Map<Item_OrderResponse>(this);
            return res;
        }

        [Required]
        public required Guid Sub_OrderId { get; set; }

        [Required]
        public required Guid ProductId { get; set; }

        [Required]
        public required int Quantity { get; set; }
        public bool IsRating { get; set; }

        public Product? Product { get; set; }
        public Sub_Order? Sub_Order { get; set; }
    }
}
