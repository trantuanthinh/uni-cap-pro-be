using uni_cap_pro_be.Data;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be
{
	public class DatabaseSeeder
	{
		private readonly DataContext _dataContext;

		public DatabaseSeeder(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public void SeedDataContext()
		{
			try
			{
				// Ensure the database is created
				_dataContext.Database.EnsureCreated();

				// Seed users
				var hashedPassword = BCrypt.Net.BCrypt.HashPassword("loveyou");

				var users = new List<(
					string Username,
					string Name,
					string Email,
					string PhoneNumber,
					UserType UserType,
					string Description
				)>
				{
					(
						"company1",
						"Company One",
						"company1@gmail.com",
						"1234567890",
						UserType.COMPANY,
						"First company"
					),
					(
						"company2",
						"Company Two",
						"company2@gmail.com",
						"0987654321",
						UserType.COMPANY,
						"Second company"
					),
					(
						"producer1",
						"Producer One",
						"producer1@gmail.com",
						"1122334455",
						UserType.PRODUCER,
						"First producer"
					),
					(
						"producer2",
						"Producer Two",
						"producer2@gmail.com",
						"5566778899",
						UserType.PRODUCER,
						"Second producer"
					)
				};

				var userList = users
					.Select(user => new User
					{
						Id = Guid.NewGuid(),
						Created_At = DateTime.UtcNow,
						Modified_At = DateTime.UtcNow,
						Username = user.Username,
						Name = user.Name,
						Email = user.Email,
						Password = hashedPassword,
						PhoneNumber = user.PhoneNumber,
						Active_Status = ActiveStatus.ACTIVE,
						User_Type = user.UserType,
						Avatar = null,
						Description = user.Description
					})
					.ToList();

				// Seed product categories
				var productCategories = new List<string>
				{
					"Fruits",
					"Vegetables",
					"Grains",
					"Dairy"
				};

				var productCategoryList = productCategories
					.Select(category => new Product_Category
					{
						Id = Guid.NewGuid(),
						Created_At = DateTime.UtcNow,
						Modified_At = DateTime.UtcNow,
						Name = category,
					})
					.ToList();

				// Seed products for each producer
				var products = new List<(
					string Name,
					Guid CategoryId,
					Product_Category Category,
					Guid OwnerId,
					User Owner,
					double Price,
					int TotalRatingValue,
					int TotalRatingQuantity,
					string Description
				)>
				{
			                 // Producer 1's products
			                 (
						"Organic Apples",
						productCategoryList[0].Id,
						productCategoryList[0],
						userList[2].Id,
						userList[2],
						30000,
						120,
						30,
						"Fresh organic apples, rich in flavor and nutrients."
					),
					(
						"Ripe Bananas",
						productCategoryList[0].Id,
						productCategoryList[0],
						userList[2].Id,
						userList[2],
						20000,
						80,
						20,
						"Sweet and ripe bananas, perfect for a healthy snack."
					),
					(
						"Organic Carrots",
						productCategoryList[1].Id,
						productCategoryList[1],
						userList[2].Id,
						userList[2],
						15000,
						90,
						20,
						"Crisp and fresh organic carrots, ideal for salads and snacking."
					),
					(
						"Fresh Broccoli",
						productCategoryList[1].Id,
						productCategoryList[1],
						userList[2].Id,
						userList[2],
						18000,
						70,
						20,
						"Nutritious and fresh broccoli, perfect for a healthy diet."
					),
					(
						"Whole Wheat Flour",
						productCategoryList[2].Id,
						productCategoryList[2],
						userList[2].Id,
						userList[2],
						12000,
						85,
						25,
						"High-quality whole wheat flour for baking and cooking."
					),
					(
						"Brown Rice",
						productCategoryList[2].Id,
						productCategoryList[2],
						userList[2].Id,
						userList[2],
						10000,
						60,
						18,
						"Nutritious brown rice, ideal for a variety of dishes."
					),
					(
						"Organic Strawberries",
						productCategoryList[0].Id,
						productCategoryList[0],
						userList[2].Id,
						userList[2],
						37000,
						110,
						22,
						"Juicy and sweet organic strawberries, perfect for desserts."
					),
					(
						"Sweet Potatoes",
						productCategoryList[1].Id,
						productCategoryList[1],
						userList[2].Id,
						userList[2],
						24000,
						65,
						14,
						"Delicious sweet potatoes, great for baking or roasting."
					),
					(
						"Bell Peppers",
						productCategoryList[1].Id,
						productCategoryList[1],
						userList[2].Id,
						userList[2],
						27500,
						65,
						13,
						"Fresh red bell peppers, ideal for salads and stir-fries."
					),
					(
						"Quinoa",
						productCategoryList[2].Id,
						productCategoryList[2],
						userList[2].Id,
						userList[2],
						31000,
						80,
						16,
						"High-protein quinoa, perfect as a side dish or main course."
					),
					(
						"Oats",
						productCategoryList[2].Id,
						productCategoryList[2],
						userList[2].Id,
						userList[2],
						18000,
						70,
						17,
						"Healthy oats for breakfast or baking."
					),
					(
						"Pineapples",
						productCategoryList[0].Id,
						productCategoryList[0],
						userList[2].Id,
						userList[2],
						36000,
						95,
						19,
						"Tropical pineapples, sweet and juicy."
					),
					(
						"Zucchini",
						productCategoryList[1].Id,
						productCategoryList[1],
						userList[2].Id,
						userList[2],
						15000,
						50,
						10,
						"Fresh zucchini, versatile for various dishes."
					),
					(
						"Fresh Milk",
						productCategoryList[3].Id,
						productCategoryList[3],
						userList[2].Id,
						userList[2],
						22000,
						90,
						20,
						"Pure and fresh milk, sourced from local dairy farms."
					),
			                 // Producer 2's products
			                 (
						"Baby Carrots",
						productCategoryList[1].Id,
						productCategoryList[1],
						userList[3].Id,
						userList[3],
						20000,
						50,
						11,
						"Sweet and tender baby carrots."
					),
					(
						"Green Beans",
						productCategoryList[1].Id,
						productCategoryList[1],
						userList[3].Id,
						userList[3],
						22000,
						55,
						13,
						"Fresh green beans, ideal for stir-fries and sides."
					),
					(
						"Millet",
						productCategoryList[2].Id,
						productCategoryList[2],
						userList[3].Id,
						userList[3],
						28000,
						50,
						14,
						"Nutritious millet, great for various recipes."
					),
					(
						"Buckwheat",
						productCategoryList[2].Id,
						productCategoryList[2],
						userList[3].Id,
						userList[3],
						34000,
						0,
						0,
						"Healthy buckwheat, a great addition to your pantry."
					),
					(
						"Mangoes",
						productCategoryList[0].Id,
						productCategoryList[0],
						userList[3].Id,
						userList[3],
						40000,
						85,
						22,
						"Sweet and juicy mangoes, perfect for smoothies and desserts."
					),
					(
						"Papayas",
						productCategoryList[0].Id,
						productCategoryList[0],
						userList[3].Id,
						userList[3],
						32000,
						75,
						18,
						"Fresh papayas, great for fruit salads and juices."
					),
					(
						"Cherry Tomatoes",
						productCategoryList[1].Id,
						productCategoryList[1],
						userList[3].Id,
						userList[3],
						25000,
						70,
						16,
						"Sweet cherry tomatoes, perfect for salads and snacks."
					),
					(
						"Cheddar Cheese",
						productCategoryList[3].Id,
						productCategoryList[3],
						userList[3].Id,
						userList[3],
						50000,
						110,
						25,
						"Aged cheddar cheese with a rich, creamy flavor."
					)
				};

				var productList = products
					.Select(product => new Product
					{
						Id = Guid.NewGuid(),
						CategoryId = product.CategoryId,
						Category = product.Category,
						OwnerId = product.OwnerId,
						Owner = product.Owner,
						Created_At = DateTime.UtcNow,
						Modified_At = DateTime.UtcNow,
						Name = product.Name,
						Price = product.Price,
						Active_Status = ActiveStatus.ACTIVE,
						Total_Rating_Value = product.TotalRatingValue,
						Total_Rating_Quantity = product.TotalRatingQuantity,
						Description = product.Description,
						Images = [],
						Discounts = [],
						Orders = [],
					})
					.ToList();

				// Seed product images
				var productImages = new List<(string URL, Guid ProductId)>
				{
			                 // Producer 1's images
			                 ("Organic Apples Image", productList[0].Id),
					("Ripe Bananas Image", productList[1].Id),
					("Organic Carrots Image", productList[2].Id),
					("Fresh Broccoli Image", productList[3].Id),
					("Whole Wheat Flour Image", productList[4].Id),
					("Brown Rice Image", productList[5].Id),
					("Organic Strawberries Image", productList[6].Id),
					("Sweet Potatoes Image", productList[7].Id),
					("Bell Peppers Image", productList[8].Id),
					("Quinoa Image", productList[9].Id),
					("Oats Image", productList[10].Id),
					("Pineapples Image", productList[11].Id),
					("Zucchini Image", productList[12].Id),
					("Fresh Milk Image", productList[13].Id),
			                 // Producer 2's images
			                 ("Baby Carrots Image", productList[14].Id),
					("Green Beans Image", productList[15].Id),
					("Millet Image", productList[16].Id),
					("Buckwheat Image", productList[17].Id),
					("Mangoes Image", productList[18].Id),
					("Papayas Image", productList[19].Id),
					("Cherry Tomatoes Image", productList[20].Id),
					("Cheddar Cheese Image", productList[21].Id)
				};

				var productImageList = productImages
					.Select(image => new Product_Image
					{
						Id = Guid.NewGuid(),
						URL = image.URL,
						Created_At = DateTime.UtcNow,
						ProductId = image.ProductId,
					})
					.ToList();

				// List of orders with specific details
				var orders = new List<(
		   Guid OwnerId,
		   ICollection<User> Owners,
		   Product Product,
		   double TotalPrice,
		   int TotalQuantity,
		   int Bundle,
		   DeliveryStatus DeliveryStatus
	   )>
		{
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 100000, 10, 1, DeliveryStatus.PENDING),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 150000, 15, 1, DeliveryStatus.PROCESSING),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 200000, 20, 4, DeliveryStatus.DELIVERED),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 120000, 12, 2, DeliveryStatus.PENDING),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 180000, 18, 5, DeliveryStatus.PROCESSING),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 170000, 17, 5, DeliveryStatus.DELIVERED),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 160000, 16, 5, DeliveryStatus.PENDING),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 140000, 14, 4, DeliveryStatus.PROCESSING),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 130000, 13, 3, DeliveryStatus.DELIVERED),
			(userList[1].Id, new List<User> { userList[1] }, productList[2], 110000, 11, 1, DeliveryStatus.PENDING)
		};

				var orderList = orders.Select(order => new Order
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Total_Price = order.TotalPrice,
					Total_Quantity = order.TotalQuantity,
					Bundle = order.Bundle,
					Timer = DateTime.UtcNow,
					Remaining_Timer = DateTime.UtcNow.AddHours(24),
					Is_Remained = true,
					Delivery_Status = order.DeliveryStatus,
					ProductId = order.Product.Id,
					Product = order.Product,
					Sub_Orders = new List<Sub_Order>()
				}).ToList();

				// List of order details with specific details
				var orderDetails = new List<(
					Guid OrderId,
					Guid ProductId,
					Guid UserId,
					Order Order,
					Product Product,
					User Owner
				)>
				{
					(
						orderList[0].Id,
						productList.First(p => p.Name == "Organic Apples").Id,
						userList.First(u => u.Username == "company1").Id,
						orderList[0],
						productList.First(p => p.Name == "Organic Apples"),
						userList.First(u => u.Username == "company1")
					),
					(
						orderList[1].Id,
						productList.First(p => p.Name == "Cheddar Cheese").Id,
						userList.First(u => u.Username == "company2").Id,
						orderList[1],
						productList.First(p => p.Name == "Cheddar Cheese"),
						userList.First(u => u.Username == "company2")
					),
					(
						orderList[2].Id,
						productList.First(p => p.Name == "Papayas").Id,
						userList.First(u => u.Username == "company1").Id,
						orderList[2],
						productList.First(p => p.Name == "Papayas"),
						userList.First(u => u.Username == "company1")
					),
					(
						orderList[3].Id,
						productList.First(p => p.Name == "Organic Carrots").Id,
						userList.First(u => u.Username == "company2").Id,
						orderList[3],
						productList.First(p => p.Name == "Organic Carrots"),
						userList.First(u => u.Username == "company2")
					),
					(
						orderList[4].Id,
						productList.First(p => p.Name == "Fresh Milk").Id,
						userList.First(u => u.Username == "company1").Id,
						orderList[4],
						productList.First(p => p.Name == "Fresh Milk"),
						userList.First(u => u.Username == "company1")
					),
					(
						orderList[5].Id,
						productList.First(p => p.Name == "Cherry Tomatoes").Id,
						userList.First(u => u.Username == "company2").Id,
						orderList[5],
						productList.First(p => p.Name == "Cherry Tomatoes"),
						userList.First(u => u.Username == "company2")
					),
					(
						orderList[6].Id,
						productList.First(p => p.Name == "Mangoes").Id,
						userList.First(u => u.Username == "company1").Id,
						orderList[6],
						productList.First(p => p.Name == "Mangoes"),
						userList.First(u => u.Username == "company1")
					),
					(
						orderList[7].Id,
						productList.First(p => p.Name == "Whole Wheat Flour").Id,
						userList.First(u => u.Username == "company2").Id,
						orderList[7],
						productList.First(p => p.Name == "Whole Wheat Flour"),
						userList.First(u => u.Username == "company2")
					),
					(
						orderList[8].Id,
						productList.First(p => p.Name == "Oats").Id,
						userList.First(u => u.Username == "company1").Id,
						orderList[8],
						productList.First(p => p.Name == "Oats"),
						userList.First(u => u.Username == "company1")
					),
					(
						orderList[9].Id,
						productList.First(p => p.Name == "Buckwheat").Id,
						userList.First(u => u.Username == "company2").Id,
						orderList[9],
						productList.First(p => p.Name == "Buckwheat"),
						userList.First(u => u.Username == "company2")
					)
				};

				//var orderDetailList = orderDetails
				//	.Select(detail => new Order_Detail
				//	{
				//		Id = Guid.NewGuid(),
				//		OrderId = detail.OrderId,
				//		ProductId = detail.ProductId,
				//		UserId = detail.UserId,
				//		Order = detail.Order,
				//		Product = detail.Product,
				//		User = detail.Owner
				//	})
				//	.ToList();

				var discountDetails = new List<(int Level, double Amount)>
				{ (1, 0.1), (2, 0.15), (3, 0.2), (4, 0.25), (5, 0.4) };

				var discountDetailList = discountDetails.Select(discount => new Discount
				{
					Id = Guid.NewGuid(),
					Level = discount.Level,
					Created_At = DateTime.Now,
					Modified_At = DateTime.Now,
					Amount = discount.Amount,
					ActiveStatus = ActiveStatus.ACTIVE,
					ProductId = productList[17].Id,
					Product = productList[17],
				}).ToList();

				// Seed Sub_Orders
				var subOrders = new List<(
					Guid UserId,
					Guid OrderId,
					int Quantity,
					double Price
				)>
		{
			(userList[1].Id, orderList[0].Id, 2, 30000),
			(userList[2].Id, orderList[1].Id, 1, 20000),
			(userList[3].Id, orderList[2].Id, 3, 90000),
            // Add more sub orders as needed
        };

				var subOrderList = subOrders
					.Select(subOrder => new Sub_Order
					{
						Id = Guid.NewGuid(),
						UserId = subOrder.UserId,
						OrderId = subOrder.OrderId,
						User = userList.First(u => u.Id == subOrder.UserId),
						Order = orderList.First(o => o.Id == subOrder.OrderId),
						Quantity = subOrder.Quantity,
						Price = subOrder.Price
					})
					.ToList();

				// Check and seed data
				_dataContext.Users.AddRange(userList);
				_dataContext.Product_Categories.AddRange(productCategoryList);
				_dataContext.Products.AddRange(productList);
				_dataContext.Product_Images.AddRange(productImageList);
				_dataContext.Orders.AddRange(orderList);
				//_dataContext.Order_Details.AddRange(orderDetailList);
				_dataContext.Discounts.AddRange(discountDetailList);
				_dataContext.Sub_Orders.AddRange(subOrderList);

				_dataContext.SaveChanges();
				Console.WriteLine("Database seeding completed.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				Console.WriteLine($"Stack Trace: {ex.StackTrace}");
			}
		}
	}
}
