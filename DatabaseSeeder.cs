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

				bool isUsers = _dataContext.Users.Any();
				bool isProducts = _dataContext.Products.Any();

				bool isSeeded = isUsers || isProducts;

				// Check if data already exists
				if (isSeeded)
				{
					Console.WriteLine("Database already seeded.");
					return;
				}

				// Seed users
				var hashedPassword = BCrypt.Net.BCrypt.HashPassword("loveyou");
				var user1 = new User
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Username = "trantuanthinh",
					Name = "Tran Tuan Thinh",
					Email = "thinh.tran.cit20@eiu.edu.vn",
					Password = hashedPassword,
					PhoneNumber = "1234567898",
					Active_Status = ActiveStatus.ACTIVE,
					User_Type = UserType.PRODUCER,
					Avatar = "https://www.google.com/imgres?q=anime%20avatar%20couple&imgurl=https%3A%2F%2Faniyuki.com%2Fwp-content%2Fuploads%2F2022%2F04%2Faniyuki-anime-couple-avatars-10.jpg&imgrefurl=https%3A%2F%2Fxaydungso.vn%2Fbai-viet-khac%2Ftop-99-cute-couple-avatar-anime-dang-gay-bao-tren-mang-vi-cb.html&docid=0p_pgPowd2i3RM&tbnid=yCyDz7nFbzicHM&vet=12ahUKEwjb1OnC0-WHAxWFslYBHcjoCdQQM3oECG4QAA..i&w=682&h=682&hcb=2&ved=2ahUKEwjb1OnC0-WHAxWFslYBHcjoCdQQM3oECG4QAA",
					Description = "trantuanthinh"
				};
				var user2 = new User
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Username = "jane_smith",
					Name = "Jane Smith",
					Email = "jane.smith@example.com",
					PhoneNumber = "02345678901",
					Password = hashedPassword,
					Active_Status = ActiveStatus.ACTIVE,
					User_Type = UserType.PRODUCER,
					Avatar = "https://www.google.com/imgres?q=anime%20avatar%20couple&imgurl=https%3A%2F%2Faniyuki.com%2Fwp-content%2Fuploads%2F2022%2F04%2Faniyuki-anime-couple-avatars-10.jpg&imgrefurl=https%3A%2F%2Fxaydungso.vn%2Fbai-viet-khac%2Ftop-99-cute-couple-avatar-anime-dang-gay-bao-tren-mang-vi-cb.html&docid=0p_pgPowd2i3RM&tbnid=yCyDz7nFbzicHM&vet=12ahUKEwjb1OnC0-WHAxWFslYBHcjoCdQQM3oECG4QAA..i&w=682&h=682&hcb=2&ved=2ahUKEwjb1OnC0-WHAxWFslYBHcjoCdQQM3oECG4QAA",
					Description = "Regular user"
				};
				var user3 = new User
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Username = "guest_user",
					Name = "Guest User",
					Email = "guest.user@example.com",
					Password = hashedPassword,
					PhoneNumber = "03456789012",
					Active_Status = ActiveStatus.ACTIVE,
					User_Type = UserType.PRODUCER,
					Avatar = null,
					Description = null
				};

				var users = new List<User> { user1, user2, user3 };
				_dataContext.Users.AddRange(users);

				var product_category1 = new Product_Category
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Crops",
					Active_Status = ActiveStatus.ACTIVE
				};

				var product_category2 = new Product_Category
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Livestock",
					Active_Status = ActiveStatus.ACTIVE
				};
				var product_category3 = new Product_Category
				{
					Id = Guid.NewGuid(),
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Dairy Products",
					Active_Status = ActiveStatus.ACTIVE
				};

				var product_categories = new List<Product_Category> { product_category1, product_category2, product_category3 };
				_dataContext.Product_Categories.AddRange(product_categories);

				var product1 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = product_category1.Id,
					OwnerId = user1.Id,
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Product A",
					Price = 99.99,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 45,
					Total_Rating_Quantity = 10
				};

				var product2 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = product_category2.Id,
					OwnerId = user2.Id,
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Product B",
					Price = 149.99,
					Active_Status = ActiveStatus.ACTIVE,
					Total_Rating_Value = 30,
					Total_Rating_Quantity = 5
				};

				var product3 = new Product
				{
					Id = Guid.NewGuid(),
					CategoryId = product_category3.Id,
					OwnerId = user3.Id,
					Created_At = DateTime.UtcNow,
					Modified_At = DateTime.UtcNow,
					Name = "Product C",
					Price = 199.99,
					Active_Status = ActiveStatus.INACTIVE,
					Total_Rating_Value = 20,
					Total_Rating_Quantity = 4
				};

				var products = new List<Product> { product1, product2, product3 };
				_dataContext.Products.AddRange(products);

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
