// using Microsoft.EntityFrameworkCore;
// using uni_cap_pro_be.Data;
// using uni_cap_pro_be.Models;
// using uni_cap_pro_be.Repositories;

// namespace uni_cap_pro_be.Services
// {
// 	// DONE
// 	public class Product_ImageService(DataContext dataContext) : IProduct_ImageService
// 	{
// 		private readonly DataContext _dataContext = dataContext;
// 		private readonly string _baseUrl = "http://localhost:5130/api/product_images/";

// 		public async Task<List<string>> GetImagesURLByProductId(Guid OwnerId, Guid ProductId)
// 		{
// 			var imagesName = await _dataContext
// 				.Product_Images.Where(item => item.ProductId == ProductId)
// 				.Select(item => item.Name)
// 				.ToListAsync();

// 			//var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources\\{OwnerId}");
// 			var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources");

// 			//if (!Directory.Exists(directoryPath))
// 			//{
// 			//	return null;
// 			//}

// 			var imageUrls = new List<string>();

// 			try
// 			{
// 				foreach (var name in imagesName)
// 				{
// 					var filePath = Path.Combine(directoryPath, name);
// 					//if (File.Exists(filePath))
// 					//{
// 					//var fileUrl = Path.Combine(_baseUrl, $"{OwnerId}/{ProductId}/{name}").Replace("\\", "/");
// 					var fileUrl = Path.Combine(_baseUrl, $"{name}").Replace("\\", "/");
// 					imageUrls.Add(fileUrl);
// 					//}
// 				}
// 			}
// 			catch (Exception ex)
// 			{
// 				Console.WriteLine("Error: ", ex);
// 				return null;
// 			}

// 			return imageUrls;
// 		}

// 		public async Task<ICollection<Product_Image>> GetImages(Guid OwnerId)
// 		{
// 			ICollection<Product_Image> _items = await _dataContext
// 				.Product_Images.OrderBy(item => item.Id)
// 				.ToListAsync();
// 			foreach (var item in _items)
// 			{
// 				var url = Path.Combine(
// 					Directory.GetCurrentDirectory(),
// 					OwnerId.ToString(),
// 					item.Name
// 				);
// 				item.Name = url;
// 			}
// 			return _items;
// 		}

// 		public async Task<Product_Image> GetImage(Guid id)
// 		{
// 			Product_Image _item = await _dataContext.Product_Images.SingleOrDefaultAsync(item =>
// 				item.Id == id
// 			);
// 			Product product = await _dataContext.Products.SingleOrDefaultAsync(item =>
// 				item.Id == _item.ProductId
// 			);

// 			if (product != null)
// 			{
// 				_item.Product = product;
// 			}
// 			return _item;
// 		}

// 		public async Task<bool> CreateImage(Product_Image item)
// 		{
// 			_dataContext.Product_Images.Add(item);
// 			return Save();
// 		}

// 		public async Task<bool> DeleteImage(Product_Image _item)
// 		{
// 			_dataContext.Product_Images.Remove(_item);
// 			return Save();
// 		}

// 		private bool Save()
// 		{
// 			int saved = _dataContext.SaveChanges();
// 			return saved > 0;
// 		}
// 	}
// }
