using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Product : BaseEntity<Guid>
    {
        // Mapping from Product to ProductResponse
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Product, ProductResponse>()
                .ForMember(d => d.Owner, d => d.MapFrom(opt => opt.Owner.Name))
                .ForMember(d => d.Category, d => d.MapFrom(opt => opt.Category.Name))
                .ForMember(d => d.Discount, d => d.MapFrom(opt => opt.Discount.ToResponse()))
                // TODO
                .ForMember(
                    d => d.Images,
                    d => d.MapFrom(opt => opt.GetImagesURLByProductAsync(opt))
                )
        );

        private List<string> GetImagesURLByProductAsync(Product opt)
        {
            string _host = "http://localhost:5130/api/product_images/";
            // Ensure product has images
            if (opt.Images == null || !opt.Images.Any())
            {
                return new List<string>();
            }

            var directoryPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"Resources\\{opt.OwnerId}"
            );

            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                return new List<string>(); // Return an empty list if the directory does not exist
            }

            var imageUrls = new List<string>();

            try
            {
                foreach (var name in opt.Images)
                {
                    var filePath = Path.Combine(directoryPath, name.ToString());
                    if (File.Exists(filePath))
                    {
                        // Construct the file URL and add to the list
                        var fileUrl = Path.Combine(_host, $"{opt.OwnerId}/{name}")
                            .Replace("\\", "/");
                        imageUrls.Add(fileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Optionally log the exception
                return new List<string>(); // Return an empty list on error
            }

            return imageUrls;
        }

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
