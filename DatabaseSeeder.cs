using uni_cap_pro_be.Data;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be
{
	public class DatabaseSeeder(DataContext dataContext)
	{
		private readonly DataContext _dataContext = dataContext;

		public void SeedDataContext()
		{
			try
			{
				// Ensure the database is created
				_dataContext.Database.EnsureCreated();

				// Seed users
				var hashedPassword = BCrypt.Net.BCrypt.HashPassword("loveyou");
				var user_company_1 = new User
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Username = "company1",
					Name = "Company One",
					Email = "company1@gmail.com",
					Password = hashedPassword,
					PhoneNumber = "1234567890",
					Active_Status = ActiveStatus.ACTIVE,
					User_Type = UserType.COMPANY,
					Avatar = null,
					Description = "First company"
				};

				var user_company_2 = new User
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Username = "company2",
					Name = "Company Two",
					Email = "company2@gmail.com",
					Password = hashedPassword,
					PhoneNumber = "0987654321",
					Active_Status = ActiveStatus.ACTIVE,
					User_Type = UserType.COMPANY,
					Avatar = null,
					Description = "Second company"
				};

				var user_producer_1 = new User
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Username = "producer1",
					Name = "Producer One",
					Email = "producer1@gmail.com",
					Password = hashedPassword,
					PhoneNumber = "1122334455",
					Active_Status = ActiveStatus.ACTIVE,
					User_Type = UserType.PRODUCER,
					Avatar = null,
					Description = "First producer"
				};

				var user_producer_2 = new User
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Username = "producer2",
					Name = "Producer Two",
					Email = "producer2@gmail.com",
					Password = hashedPassword,
					PhoneNumber = "5566778899",
					Active_Status = ActiveStatus.ACTIVE,
					User_Type = UserType.PRODUCER,
					Avatar = null,
					Description = "Second producer"
				};

				var users = new List<User>
				{
					user_company_1,
					user_company_2,
					user_producer_1,
					user_producer_2
				};

				// Seed product categories
				var product_category_1 = new Product_Category
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Fruits",
					Active_Status = ActiveStatus.ACTIVE
				};

				var product_category_2 = new Product_Category
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Vegetables",
					Active_Status = ActiveStatus.ACTIVE
				};

				var product_category_3 = new Product_Category
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Grains",
					Active_Status = ActiveStatus.ACTIVE
				};

				var product_category_4 = new Product_Category
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Dairy",
					Active_Status = ActiveStatus.ACTIVE
				};
				var productCategories = new List<Product_Category>
				{
					product_category_1,
					product_category_2,
					product_category_3,
					product_category_4
				};

				// Seed products for each producer
				// Producer 1's products
				var product1 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[0].Id, // Fruits
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Organic Apples",
					Price = 30000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 120,
					Total_Rating_Quantity = 30,
					Description = "Fresh organic apples, rich in flavor and nutrients."
				};

				var product2 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[0].Id, // Fruits
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Ripe Bananas",
					Price = 20000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 80,
					Total_Rating_Quantity = 20,
					Description = "Sweet and ripe bananas, perfect for a healthy snack."
				};

				var product3 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[1].Id, // Vegetables
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Organic Carrots",
					Price = 15000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 90,
					Total_Rating_Quantity = 20,
					Description = "Crisp and fresh organic carrots, ideal for salads and snacking."
				};

				var product4 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[1].Id, // Vegetables
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Fresh Broccoli",
					Price = 18000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 70,
					Total_Rating_Quantity = 20,
					Description = "Nutritious and fresh broccoli, perfect for a healthy diet."
				};

				var product5 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[2].Id, // Grains
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Whole Wheat Flour",
					Price = 12000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 85,
					Total_Rating_Quantity = 25,
					Description = "High-quality whole wheat flour for baking and cooking."
				};

				var product6 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[2].Id, // Grains
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Brown Rice",
					Price = 10000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 60,
					Total_Rating_Quantity = 18,
					Description = "Nutritious brown rice, ideal for a variety of dishes."
				};

				var product7 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[0].Id, // Fruits
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Organic Strawberries",
					Price = 37000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 110,
					Total_Rating_Quantity = 22,
					Description = "Juicy and sweet organic strawberries, perfect for desserts."
				};

				var product8 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[1].Id, // Vegetables
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Sweet Potatoes",
					Price = 24000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 65,
					Total_Rating_Quantity = 14,
					Description = "Delicious sweet potatoes, great for baking or roasting."
				};

				var product9 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[1].Id, // Vegetables
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Bell Peppers",
					Price = 27500,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 65,
					Total_Rating_Quantity = 13,
					Description = "Fresh red bell peppers, ideal for salads and stir-fries."
				};

				var product10 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[2].Id, // Grains
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Quinoa",
					Price = 31000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 80,
					Total_Rating_Quantity = 16,
					Description = "High-protein quinoa, perfect as a side dish or main course."
				};

				var product11 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[2].Id, // Grains
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Oats",
					Price = 18000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 70,
					Total_Rating_Quantity = 17,
					Description = "Healthy oats for breakfast or baking."
				};

				var product12 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[0].Id, // Fruits
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Pineapples",
					Price = 36000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 95,
					Total_Rating_Quantity = 19,
					Description = "Tropical pineapples, sweet and juicy."
				};

				var product13 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[1].Id, // Vegetables
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Zucchini",
					Price = 15000, // VND
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 50,
					Total_Rating_Quantity = 10,
					Description = "Fresh zucchini, versatile for various dishes."
				};

				var product14 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[3].Id, // Dairy
					OwnerId = users[2].Id, // Producer 1
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Fresh Milk",
					Price = 22000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 90,
					Total_Rating_Quantity = 20,
					Description = "Pure and fresh milk, sourced from local dairy farms."
				};

				// Producer 2's products
				var product15 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[1].Id, // Vegetables
					OwnerId = users[3].Id, // Producer 2
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Baby Carrots",
					Price = 20000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 50,
					Total_Rating_Quantity = 11,
					Description = "Sweet and tender baby carrots."
				};

				var product16 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[1].Id, // Vegetables
					OwnerId = users[3].Id, // Producer 2
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Green Beans",
					Price = 22000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 55,
					Total_Rating_Quantity = 13,
					Description = "Fresh green beans, ideal for stir-fries and sides."
				};

				var product17 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[2].Id, // Grains
					OwnerId = users[3].Id, // Producer 2
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Millet",
					Price = 28000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 50,
					Total_Rating_Quantity = 14,
					Description = "Nutritious millet, great for various recipes."
				};

				var product18 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[2].Id, // Grains
					OwnerId = users[3].Id, // Producer 2
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Buckwheat",
					Price = 34000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 0,
					Total_Rating_Quantity = 0,
					Description = "Healthy buckwheat, a great addition to your pantry."
				};

				var product19 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[0].Id, // Fruits
					OwnerId = users[3].Id, // Producer 2
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Mangoes",
					Price = 40000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 85,
					Total_Rating_Quantity = 22,
					Description = "Sweet and juicy mangoes, perfect for smoothies and desserts."
				};

				var product20 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[0].Id, // Fruits
					OwnerId = users[3].Id, // Producer 2
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Papayas",
					Price = 32000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 75,
					Total_Rating_Quantity = 18,
					Description = "Fresh papayas, great for fruit salads and juices."
				};

				var product21 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[1].Id, // Vegetables
					OwnerId = users[3].Id, // Producer 2
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Cherry Tomatoes",
					Price = 25000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 70,
					Total_Rating_Quantity = 16,
					Description = "Sweet cherry tomatoes, perfect for salads and snacks."
				};

				var product22 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = productCategories[3].Id, // Dairy
					OwnerId = users[3].Id, // Producer 2
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Cheddar Cheese",
					Price = 50000,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 110,
					Total_Rating_Quantity = 25,
					Description = "Aged cheddar cheese with a rich, creamy flavor."
				};
				var productList = new List<Product>
				{
					product1,
					product2,
					product3,
					product4,
					product5,
					product6,
					product7,
					product8,
					product9,
					product10,
					product11,
					product12,
					product13,
					product14,
					product15,
					product16,
					product17,
					product18,
					product19,
					product20,
					product21,
					product22
				};

				bool isUsers = _dataContext.Users.Any();
				bool isProduct_Categories = _dataContext.Product_Categories.Any();
				bool isProducts = _dataContext.Products.Any();
				bool isProduct_Images = _dataContext.Product_Images.Any();

				if (!isUsers)
				{
					_dataContext.Users.AddRange(users);
				}
				if (!isProduct_Categories)
				{
					_dataContext.Product_Categories.AddRange(productCategories);
				}
				if (!isProducts)
				{
					_dataContext.Products.AddRange(productList);
				}
				if (!isProduct_Images)
				{
					_dataContext.Product_Images.AddRange();
				}

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
