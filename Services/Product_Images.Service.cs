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
    }
}
