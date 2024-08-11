using System;
using System.Collections.Generic;
using System.Linq;
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
                        Active_Status = ActiveStatus.ACTIVE
                    })
                    .ToList();

                // Seed products for each producer
                var products = new List<(
                    string Name,
                    Guid CategoryId,
                    Guid OwnerId,
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
                        userList[2].Id,
                        30000,
                        120,
                        30,
                        "Fresh organic apples, rich in flavor and nutrients."
                    ),
                    (
                        "Ripe Bananas",
                        productCategoryList[0].Id,
                        userList[2].Id,
                        20000,
                        80,
                        20,
                        "Sweet and ripe bananas, perfect for a healthy snack."
                    ),
                    (
                        "Organic Carrots",
                        productCategoryList[1].Id,
                        userList[2].Id,
                        15000,
                        90,
                        20,
                        "Crisp and fresh organic carrots, ideal for salads and snacking."
                    ),
                    (
                        "Fresh Broccoli",
                        productCategoryList[1].Id,
                        userList[2].Id,
                        18000,
                        70,
                        20,
                        "Nutritious and fresh broccoli, perfect for a healthy diet."
                    ),
                    (
                        "Whole Wheat Flour",
                        productCategoryList[2].Id,
                        userList[2].Id,
                        12000,
                        85,
                        25,
                        "High-quality whole wheat flour for baking and cooking."
                    ),
                    (
                        "Brown Rice",
                        productCategoryList[2].Id,
                        userList[2].Id,
                        10000,
                        60,
                        18,
                        "Nutritious brown rice, ideal for a variety of dishes."
                    ),
                    (
                        "Organic Strawberries",
                        productCategoryList[0].Id,
                        userList[2].Id,
                        37000,
                        110,
                        22,
                        "Juicy and sweet organic strawberries, perfect for desserts."
                    ),
                    (
                        "Sweet Potatoes",
                        productCategoryList[1].Id,
                        userList[2].Id,
                        24000,
                        65,
                        14,
                        "Delicious sweet potatoes, great for baking or roasting."
                    ),
                    (
                        "Bell Peppers",
                        productCategoryList[1].Id,
                        userList[2].Id,
                        27500,
                        65,
                        13,
                        "Fresh red bell peppers, ideal for salads and stir-fries."
                    ),
                    (
                        "Quinoa",
                        productCategoryList[2].Id,
                        userList[2].Id,
                        31000,
                        80,
                        16,
                        "High-protein quinoa, perfect as a side dish or main course."
                    ),
                    (
                        "Oats",
                        productCategoryList[2].Id,
                        userList[2].Id,
                        18000,
                        70,
                        17,
                        "Healthy oats for breakfast or baking."
                    ),
                    (
                        "Pineapples",
                        productCategoryList[0].Id,
                        userList[2].Id,
                        36000,
                        95,
                        19,
                        "Tropical pineapples, sweet and juicy."
                    ),
                    (
                        "Zucchini",
                        productCategoryList[1].Id,
                        userList[2].Id,
                        15000,
                        50,
                        10,
                        "Fresh zucchini, versatile for various dishes."
                    ),
                    (
                        "Fresh Milk",
                        productCategoryList[3].Id,
                        userList[2].Id,
                        22000,
                        90,
                        20,
                        "Pure and fresh milk, sourced from local dairy farms."
                    ),
                    // Producer 2's products
                    (
                        "Baby Carrots",
                        productCategoryList[1].Id,
                        userList[3].Id,
                        20000,
                        50,
                        11,
                        "Sweet and tender baby carrots."
                    ),
                    (
                        "Green Beans",
                        productCategoryList[1].Id,
                        userList[3].Id,
                        22000,
                        55,
                        13,
                        "Fresh green beans, ideal for stir-fries and sides."
                    ),
                    (
                        "Millet",
                        productCategoryList[2].Id,
                        userList[3].Id,
                        28000,
                        50,
                        14,
                        "Nutritious millet, great for various recipes."
                    ),
                    (
                        "Buckwheat",
                        productCategoryList[2].Id,
                        userList[3].Id,
                        34000,
                        0,
                        0,
                        "Healthy buckwheat, a great addition to your pantry."
                    ),
                    (
                        "Mangoes",
                        productCategoryList[0].Id,
                        userList[3].Id,
                        40000,
                        85,
                        22,
                        "Sweet and juicy mangoes, perfect for smoothies and desserts."
                    ),
                    (
                        "Papayas",
                        productCategoryList[0].Id,
                        userList[3].Id,
                        32000,
                        75,
                        18,
                        "Fresh papayas, great for fruit salads and juices."
                    ),
                    (
                        "Cherry Tomatoes",
                        productCategoryList[1].Id,
                        userList[3].Id,
                        25000,
                        70,
                        16,
                        "Sweet cherry tomatoes, perfect for salads and snacks."
                    ),
                    (
                        "Cheddar Cheese",
                        productCategoryList[3].Id,
                        userList[3].Id,
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
                        OwnerId = product.OwnerId,
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = product.Name,
                        Price = product.Price,
                        Active_Status = ActiveStatus.ACTIVE,
                        Total_Rating_Value = product.TotalRatingValue,
                        Total_Rating_Quantity = product.TotalRatingQuantity,
                        Description = product.Description
                    })
                    .ToList();

                // Seed product images
                var productImages = new List<(string Name, Guid ProductId)>
                {
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
                        Name = image.Name,
                        Created_At = DateTime.UtcNow,
                        ProductId = image.ProductId
                    })
                    .ToList();

                // Check and seed data
                if (!_dataContext.Users.Any())
                {
                    _dataContext.Users.AddRange(userList);
                }
                if (!_dataContext.Product_Categories.Any())
                {
                    _dataContext.Product_Categories.AddRange(productCategoryList);
                }
                if (!_dataContext.Products.Any())
                {
                    _dataContext.Products.AddRange(productList);
                }
                if (!_dataContext.Product_Images.Any())
                {
                    _dataContext.Product_Images.AddRange(productImageList);
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
