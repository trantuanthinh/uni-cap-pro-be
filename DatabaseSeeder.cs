using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using uni_cap_pro_be;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Services;
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
                var users = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Username = "company1",
                        Name = "Company One",
                        Email = "company1@example.com",
                        Password = hashedPassword,
                        PhoneNumber = "1234567890",
                        Active_Status = ActiveStatus.ACTIVE,
                        User_Type = UserType.COMPANY,
                        Avatar = null,
                        Description = "First company"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Username = "company2",
                        Name = "Company Two",
                        Email = "company2@example.com",
                        Password = hashedPassword,
                        PhoneNumber = "0987654321",
                        Active_Status = ActiveStatus.ACTIVE,
                        User_Type = UserType.COMPANY,
                        Avatar = null,
                        Description = "Second company"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Username = "producer1",
                        Name = "Producer One",
                        Email = "producer1@example.com",
                        Password = hashedPassword,
                        PhoneNumber = "1122334455",
                        Active_Status = ActiveStatus.ACTIVE,
                        User_Type = UserType.PRODUCER,
                        Avatar = null,
                        Description = "First producer"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Username = "producer2",
                        Name = "Producer Two",
                        Email = "producer2@example.com",
                        Password = hashedPassword,
                        PhoneNumber = "5566778899",
                        Active_Status = ActiveStatus.ACTIVE,
                        User_Type = UserType.PRODUCER,
                        Avatar = null,
                        Description = "Second producer"
                    }
                };
                _dataContext.Users.AddRange(users);

                // Seed product categories
                var productCategories = new List<Product_Category>
                {
                    new Product_Category
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = "Fruits",
                        Active_Status = ActiveStatus.ACTIVE
                    },
                    new Product_Category
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = "Vegetables",
                        Active_Status = ActiveStatus.ACTIVE
                    },
                    new Product_Category
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = "Grains",
                        Active_Status = ActiveStatus.ACTIVE
                    },
                    new Product_Category
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = "Dairy Products",
                        Active_Status = ActiveStatus.ACTIVE
                    },
                    new Product_Category
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = "Meat",
                        Active_Status = ActiveStatus.ACTIVE
                    }
                };
                _dataContext.Product_Categories.AddRange(productCategories);

                // Seed products for each producer
                var productList = new List<Product>();

                for (int i = 1; i <= 10; i++)
                {
                    productList.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = productCategories[i % productCategories.Count].Id,
                        OwnerId = users[2].Id, // Producer 1
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = $"Producer1_Product{i}",
                        Price = 10 + i * 5,
                        Active_Status = ActiveStatus.ACTIVE,
                        Total_Rating_Value = i * 2,
                        Total_Rating_Quantity = i,
                        Image = $"https://example.com/images/producer1_product{i}.jpg",
                        Description = $"Description for Producer 1 Product {i}"
                    });

                    productList.Add(new Product
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = productCategories[(i + 1) % productCategories.Count].Id,
                        OwnerId = users[3].Id, // Producer 2
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = $"Producer2_Product{i}",
                        Price = 20 + i * 5,
                        Active_Status = ActiveStatus.ACTIVE,
                        Total_Rating_Value = i * 3,
                        Total_Rating_Quantity = i + 1,
                        Image = $"https://example.com/images/producer2_product{i}.jpg",
                        Description = $"Description for Producer 2 Product {i}"
                    });
                }

                _dataContext.Products.AddRange(productList);

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
