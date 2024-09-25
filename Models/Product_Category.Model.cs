using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Product_Category : BaseEntity<Guid>
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product_Category, Product_CategoryResponse>();
        });

        static readonly IMapper mapper = new Mapper(config);

        public Product_CategoryResponse ToResponse()
        {
            var res = mapper.Map<Product_CategoryResponse>(this);
            return res;
        }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
