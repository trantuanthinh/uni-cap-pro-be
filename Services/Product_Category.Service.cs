// using Microsoft.EntityFrameworkCore;
// using uni_cap_pro_be.Data;
// using uni_cap_pro_be.Models;
// using uni_cap_pro_be.Repositories;

// namespace uni_cap_pro_be.Services
// {
// 	// DONE
// 	public class Product_CategoryService(DataContext dataContext) : IProduct_CategoryService
// 	{
// 		private readonly DataContext _dataContext = dataContext;

// 		public async Task<ICollection<Product_Category>> GetProduct_Categories()
// 		{
// 			ICollection<Product_Category> _item = await _dataContext
// 				.Product_Categories.OrderBy(item => item.Id)
// 				.ToListAsync();
// 			return _item;
// 		}

// 		public async Task<Product_Category> GetProduct_Category(Guid id)
// 		{
// 			Product_Category _item = await _dataContext.Product_Categories.SingleOrDefaultAsync(
// 				item => item.Id == id
// 			);
// 			return _item;
// 		}

// 		public async Task<bool> CreateProduct_Category(Product_Category _item)
// 		{
// 			_item.Created_At = DateTime.UtcNow;
// 			_item.Modified_At = DateTime.UtcNow;
// 			_dataContext.Product_Categories.Add(_item);
// 			return Save();
// 		}

// 		public async Task<bool> UpdateProduct_Category(
// 			Product_Category _item,
// 			Product_Category patchProduct_Category
// 		)
// 		{
// 			_item.Modified_At = DateTime.UtcNow;
// 			_dataContext.Product_Categories.Update(_item);
// 			return Save();
// 		}

// 		public async Task<bool> DeleteProduct_Category(Product_Category _item)
// 		{
// 			_dataContext.Product_Categories.Remove(_item);
// 			return Save();
// 		}

// 		private bool Save()
// 		{
// 			int saved = _dataContext.SaveChanges();
// 			return saved > 0;
// 		}
// 	}
// }
