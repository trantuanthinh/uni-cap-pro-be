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

				// seed discount
				var discountList = new List<Discount>
{
	new Discount
	{
		Id = Guid.NewGuid(),
		Created_At = DateTime.Now,
		Modified_At = DateTime.Now,
		ActiveStatus = ActiveStatus.ACTIVE
	}
};

				var discountDetails = new List<(int Level, double Amount)>
{
	(1, 0.1),
	(2, 0.15),
	(3, 0.2),
	(4, 0.25),
	(5, 0.4)
};

				var discountDetailList = discountDetails.Select(detail => new Discount_Detail
				{
					DiscountId = discountList[0].Id,
					Level = detail.Level,
					Amount = detail.Amount,
					//Discount = discountList[0],
				}).ToList();

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
						DiscountId = discountList[0].Id,
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
						Discount = discountList[0]
					}).ToList();

				// Seed product images
				// Images for Producer 1
				var productImages = new List<(string Name, Guid ProductId, Product Product)>
{
	("Organic Apples.jpg", productList[0].Id, productList[0]),
	("Ripe Bananas.jpg", productList[1].Id, productList[1]),
	("Organic Carrots.jpg", productList[2].Id, productList[2]),
	("Fresh Broccoli.jpg", productList[3].Id, productList[3]),
	("Whole Wheat Flour.jpg", productList[4].Id, productList[4]),
	("Brown Rice.jpg", productList[5].Id, productList[5]),
	("Organic Strawberries.jpg", productList[6].Id, productList[6]),
	("Sweet Potatoes.jpg", productList[7].Id, productList[7]),
	("Bell Peppers.jpg", productList[8].Id, productList[8]),
	("Quinoa.jpg", productList[9].Id, productList[9]),
	("Oats.jpg", productList[10].Id, productList[10]),
	("Pineapples.jpg", productList[11].Id, productList[11]),
	("Zucchini.jpg", productList[12].Id, productList[12]),
	("Fresh Milk.jpg", productList[13].Id, productList[13]),
    // Producer 2's images
    ("Baby Carrots.jpg", productList[14].Id, productList[14]),
	("Green Beans.jpg", productList[15].Id, productList[15]),
	("Millet.jpg", productList[16].Id, productList[16]),
	("Buckwheat.jpg", productList[17].Id, productList[17]),
	("Mangoes.jpg", productList[18].Id, productList[18]),
	("Papayas.jpg", productList[19].Id, productList[19]),
	("Cherry Tomatoes.jpg", productList[20].Id, productList[20]),
	("Cheddar Cheese.jpg", productList[21].Id, productList[21])
};


				var productImageList = productImages
					.Select(image => new Product_Image
					{
						Id = Guid.NewGuid(),
						Name = image.Name,
						Created_At = DateTime.UtcNow,
						ProductId = image.ProductId,
						Product = image.Product
					})
					.ToList();

				// seed order
				var orders = new List<(Guid ProductId, double Total_Price, Product Product)> {
				(               productList[0].Id,productList[0].Price, productList[0]              )               };
				var orderList = orders.Select(order => new Order
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.Now,
					Modified_At = DateTime.Now,
					ProductId = order.ProductId,
					Total_Price = order.Total_Price,
					Total_Quantity = 1,
					Timer = DateTime.UtcNow.AddDays(1) - DateTime.UtcNow,
					Remaining_Timer = DateTime.UtcNow.AddDays(1),
					Level = 1,
					Delivery_Status = DeliveryStatus.PENDING,
					Product = order.Product,
				}).ToList();

				// seed sub order
				var sub_orders = new List<(Guid UserId, User User, Guid OrderId, Order Order, int Quantity, double Price)>
				{(userList[0].Id,userList[0],orderList[0].Id,orderList[0],1,productList[0].Price)};
				var sub_orderList = sub_orders.Select(sub_order => new Sub_Order
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.Now,
					Modified_At = DateTime.Now,
					UserId = sub_order.UserId,
					OrderId = sub_order.OrderId,
					Quantity = sub_order.Quantity,
					Price = sub_order.Price,
					User = sub_order.User,
					Order = sub_order.Order,
				}).ToList();

				// Check and seed data
				_dataContext.Users.AddRange(userList);
				_dataContext.Product_Categories.AddRange(productCategoryList);
				_dataContext.Products.AddRange(productList);
				_dataContext.Product_Images.AddRange(productImageList);

				_dataContext.Orders.AddRange(orderList);
				_dataContext.Sub_Orders.AddRange(sub_orderList);

				_dataContext.Discounts.AddRange(discountList);
				_dataContext.Discount_Details.AddRange(discountDetailList);

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