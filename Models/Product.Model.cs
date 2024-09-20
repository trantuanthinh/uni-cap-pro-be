using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Product : BaseEntity<Guid>
    {
        // Mapping from Product to ProductResponse
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Product, ProductResponse>()
                .ForMember(d => d.Owner, d => d.MapFrom(t => t.Owner.Name))
                .ForMember(d => d.Category, d => d.MapFrom(t => t.Category.Name))
        // .ForMember(
        //     d => d.Discount,
        //     d => d.MapFrom(t => t.Discount.Discount_Details)
        // )
        // .ForMember(
        //     d => d.Images,
        //     d => d.MapFrom(t => t.Images.Select(e => e.Name).ToList())
        // )
        );

        static readonly IMapper mapper = config.CreateMapper();

        public ProductResponse ToResponse()
        {
            var res = mapper.Map<ProductResponse>(this);
            return res;
        }

        [Required]
        public required Guid CategoryId { get; set; }

        [Required]
        public required Guid DiscountId { get; set; }

        [Required]
        public required Guid OwnerId { get; set; } // the owner of the product

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required double Price { get; set; }
        public string? Description { get; set; }

        [Required]
        public required ActiveStatus Active_Status { get; set; }

        public int Total_Rating_Value { get; set; } // the total number of stars which is rated by user
        public int Total_Rating_Quantity { get; set; } // the total number of user who rated the product

        [Required]
        public required User Owner { get; set; } // the owner of the product
        public Product_Category? Category { get; set; }
        public Discount? Discount { get; set; }
        public ICollection<Product_Image>? Images { get; set; }
    }
}
