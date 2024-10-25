using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Models.Setting_Data_Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Product : BaseEntity<Guid>
    {
        // Mapping from Product to ProductResponse
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Product, ProductResponse>()
                .ForMember(d => d.Owner, opt => opt.MapFrom(src => src.Owner.Name))
                .ForMember(d => d.Category, opt => opt.MapFrom(src => src.Category.ToResponse()))
                .ForMember(d => d.Discount, opt => opt.MapFrom(src => src.Discount.ToResponse()))
                .ForMember(d => d.UnitMeasure, opt => opt.MapFrom(src => src.UnitMeasure.Symbol))
                // .ForMember(
                //     dest => dest.Images,
                //     opt => opt.MapFrom(src => src.Images.Select(img => img.Name).ToList()) // Map danh sách tên từ Product_Image
                // )
                .ForMember(d => d.Images, opt => opt.MapFrom(src => src.GetImageUrls()))
        );

        private List<string> GetImageUrls()
        {
            string host = "http://localhost:5130/api/product_images/";
            var imageUrls = new List<string>();

            // Ensure product has images
            if (Images == null || !Images.Any())
            {
                return imageUrls;
            }

            var directoryPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"Resources\\{OwnerId}"
            );

            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                return imageUrls; // Return an empty list if the directory does not exist
            }

            try
            {
                foreach (var image in Images)
                {
                    var filePath = Path.Combine(directoryPath, image.Name);
                    if (File.Exists(filePath))
                    {
                        // Construct the file URL and add to the list
                        var fileUrl = Path.Combine(host, $"{OwnerId}/{image.Name}")
                            .Replace("\\", "/");
                        imageUrls.Add(fileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required double Price { get; set; }

        public required double Quantity { get; set; }

        [Required]
        public required Guid UnitMeasureId { get; set; }

        [Required]
        public required Guid OwnerId { get; set; }

        public string? Description { get; set; }

        [Required]
        public required ActiveStatus Active_Status { get; set; }

        public int Total_Rating_Value { get; set; } // the total number of stars which is rated by user
        public int Total_Rating_Quantity { get; set; } // the total number of user who rated the product

        [Required]
        public required UnitMeasure UnitMeasure { get; set; }

        [Required]
        public required User Owner { get; set; }
        public Product_Category? Category { get; set; }
        public Discount? Discount { get; set; }
        public ICollection<Product_Image>? Images { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
