using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class Product_ImageService(Product_ImageRepository repository) : BaseService()
    {
        private readonly Product_ImageRepository _repository = repository;
        private readonly string _host = "http://localhost:5130/api/product_images/";

        public async Task<string> GetImagePath(string ownerId, string name)
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Resources",
                ownerId,
                name
            );
            return filePath;
        }

        // public async Task<List<string>> GetImagesURLByProduct(Product product)
        // {
        //     // Ensure product has images
        //     if (product.Images == null || !product.Images.Any())
        //     {
        //         return new List<string>();
        //     }

        //     var directoryPath = Path.Combine(
        //         Directory.GetCurrentDirectory(),
        //         $"Resources\\{product.OwnerId}"
        //     );

        //     // Check if the directory exists
        //     if (!Directory.Exists(directoryPath))
        //     {
        //         return new List<string>(); // Return an empty list if the directory does not exist
        //     }

        //     var imageUrls = new List<string>();

        //     try
        //     {
        //         foreach (var name in product.Images)
        //         {
        //             var filePath = Path.Combine(directoryPath, name.ToString());
        //             if (File.Exists(filePath))
        //             {
        //                 // Construct the file URL and add to the list
        //                 var fileUrl = Path.Combine(_host, $"{product.OwnerId}/{name}")
        //                     .Replace("\\", "/");
        //                 imageUrls.Add(fileUrl);
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Error: {ex.Message}");
        //         // Optionally log the exception
        //         return new List<string>(); // Return an empty list on error
        //     }

        //     return imageUrls;
        // }
    }
}
