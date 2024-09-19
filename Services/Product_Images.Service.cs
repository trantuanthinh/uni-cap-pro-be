using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Repositories;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class Product_ImageService(Product_ImageRepository repository) : BaseService()
    {
        private readonly Product_ImageRepository _repository = repository;
        private readonly string _host = "http://localhost:5130/api/product_images/";

        public async Task<List<string>> GetImagesURLByProductId(Guid OwnerId, Guid ProductId)
        {
            var imagesName = await _repository
                .GetDbSet()
                .Where(item => item.ProductId == ProductId)
                .Select(item => item.Name)
                .ToListAsync();

            var directoryPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"Resources\\{OwnerId}"
            );
            // var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources");

            if (!Directory.Exists(directoryPath))
            {
                return null;
            }

            var imageUrls = new List<string>();

            try
            {
                foreach (var name in imagesName)
                {
                    var filePath = Path.Combine(directoryPath, name);
                    if (File.Exists(filePath))
                    {
                        var fileUrl = Path.Combine(_host, $"{OwnerId}/{ProductId}/{name}")
                            .Replace("\\", "/");
                        // var fileUrl = Path.Combine(_host, $"{name}").Replace("\\", "/");
                        imageUrls.Add(fileUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex);
                return null;
            }

            return imageUrls;
        }
    }
}
