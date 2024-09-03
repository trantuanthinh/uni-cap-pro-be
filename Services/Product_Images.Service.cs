using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	// DONE
	public class Product_ImageService<T> : IProduct_ImageService<T>
		where T : Product_Image
	{
		private readonly DataContext _dataContext;
		private readonly DbSet<T> _dataSet;
		private readonly SharedService _sharedService;
		private readonly string _baseUrl = "http://localhost:5130/api/product_images/";

		public Product_ImageService(DataContext dataContext, SharedService sharedService)
		{
			_dataContext = dataContext;
			_dataSet = _dataContext.Set<T>();
			_sharedService = sharedService;
		}

		public async Task<List<string>> GetImagesURLByProductId(Guid OwnerId, Guid ProductId)
		{
			var imagesName = await _dataSet
				.Where(item => item.ProductId == ProductId)
				.Select(item => item.Name)
				.ToListAsync();

			//var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources\\{OwnerId}\\{ProductId}");
			var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources");


			//if (!Directory.Exists(directoryPath))
			//{
			//	return null;
			//}

			var imageUrls = new List<string>();

			try
			{
				foreach (var name in imagesName)
				{
					var filePath = Path.Combine(directoryPath, name);
					//if (File.Exists(filePath))
					//{
					//var fileUrl = Path.Combine(_baseUrl, $"{OwnerId}/{ProductId}/{name}").Replace("\\", "/");
					var fileUrl = Path.Combine(_baseUrl, $"{name}").Replace("\\", "/");
					imageUrls.Add(fileUrl);
					//}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: ", ex);
				return null;
			}

			return imageUrls;
		}



		public async Task<ICollection<T>> GetItems(Guid OwnerId)
		{
			ICollection<T> _items = await _dataSet.OrderBy(item => item.Id).ToListAsync();
			foreach (var item in _items)
			{
				var url = Path.Combine(Directory.GetCurrentDirectory(), OwnerId.ToString(), item.ProductId.ToString(), item.Name);
				item.Name = url;
			}
			return _items;
		}

		public async Task<T> GetItem(Guid id, Guid OwnerId)
		{
			T _item = await _dataSet.SingleOrDefaultAsync(item => item.Id == id);
			var url = Path.Combine(Directory.GetCurrentDirectory(), OwnerId.ToString(), _item.ProductId.ToString(), _item.Name);
			_item.Name = url;
			return _item;
		}

		public async Task<bool> CreateItem(T _item)
		{
			_item.Created_At = DateTime.UtcNow;
			_dataSet.Add(_item);
			return Save();
		}

		public async Task<bool> UpdateItem(T _item, T patchItem)
		{
			_dataSet.Update(_item);
			return Save();
		}

		public async Task<bool> DeleteItem(T _item)
		{
			_dataSet.Remove(_item);
			return Save();
		}

		public bool Save()
		{
			int saved = _dataContext.SaveChanges();
			return saved > 0;
		}

		private string GetImageUrl(Product_Image item)
		{
			string baseUrl = "https://example.com/images/";

			return baseUrl + item.Name;
		}

		public Task<ICollection<T>> GetItems()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetItem(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
