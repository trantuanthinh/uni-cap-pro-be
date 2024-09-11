using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
    // DONE
    public interface IProductService
    {
        Task<ICollection<Product>> GetProducts();
        Task<Product> GetProduct(Guid id);
        Task<bool> CreateProduct(Product item);
        Task<bool> UpdateProduct(Product item, Product patchItem);
        Task<bool> DeleteProduct(Product item);
        // public Task<List<Product>> GetSubOrdersById(Guid OrderId);
    }
}
