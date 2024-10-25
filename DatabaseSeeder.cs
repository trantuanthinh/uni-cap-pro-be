using uni_cap_pro_be.Data;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Models.Setting_Data_Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be
{
    // DONE
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

                #region seed settings-data
                var unitMeasures = new List<(string Name, string Symbol)>
                {
                    ("Kilogram", "kg"),
                    ("Liter", "l"),
                };
                var unitMeasureList = unitMeasures
                    .Select(um => new UnitMeasure
                    {
                        Id = Guid.NewGuid(),
                        Name = um.Name,
                        Symbol = um.Symbol
                    })
                    .ToList();
                #endregion

                #region seed discounts
                var discountList = new List<Discount>
                {
                    new Discount { Id = Guid.NewGuid(), Active_Status = ActiveStatus.ACTIVE }
                };
                var discountDetails = new List<(int Level, double Amount)>
                {
                    (1, 0),
                    (2, 0.1),
                    (3, 0.15),
                    (4, 0.2),
                    (5, 0.25),
                    (6, 0.4)
                };

                var discountDetailList = discountDetails
                    .Select(detail => new Discount_Detail
                    {
                        Id = Guid.NewGuid(),
                        DiscountId = discountList[0].Id,
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Level = detail.Level,
                        Amount = detail.Amount,
                    })
                    .ToList();
                #endregion

                #region seed users
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword("thaohoang");

                var users = new List<(
                    string Username,
                    string Name,
                    string Email,
                    string PhoneNumber,
                    string Description,
                    UserType Type
                )>
                {
                    (
                        "trantuanthinh",
                        "Tran Tuan Thinh",
                        "tran.tuan.thinh.0125@gmail.com",
                        "0395279915",
                        "Tran Tuan Thinh",
                        UserType.COMPANY
                    ),
                    (
                        "thaohoang",
                        "Hoang Thi Thanh Thao",
                        "thaoh1717@gmail.com",
                        "0327858682",
                        "ThaoHoang1717",
                        UserType.COMPANY
                    ),
                    (
                        "company1",
                        "Company 1",
                        "company1@gmail.com",
                        "1234567890",
                        "Company 1",
                        UserType.COMPANY
                    ),
                    (
                        "company2",
                        "Company 2",
                        "company2@gmail.com",
                        "9876543210",
                        "Company 2",
                        UserType.COMPANY
                    ),
                    (
                        "producer1",
                        "Producer 1",
                        "producer1@gmail.com",
                        "1237894560",
                        "Producer 1",
                        UserType.PRODUCER
                    ),
                    (
                        "producer2",
                        "Producer 2",
                        "producer2@gmail.com",
                        "9873216540",
                        "Producer 2",
                        UserType.PRODUCER
                    ),
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
                        Type = user.Type,
                        Avatar = null,
                        Description = user.Description
                    })
                    .ToList();
                #endregion

                #region seed product_categories
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
                #endregion

                #region seed products for each producer
                var products = new List<(
                    string Name,
                    Guid CategoryId,
                    Product_Category Category,
                    Guid UnitMeasureId,
                    UnitMeasure UnitMeasure,
                    double Price,
                    int TotalRatingValue,
                    int TotalRatingQuantity,
                    string Description,
                    Guid OwnerId,
                    User Owner
                )>
                {
                    // Producer 1's products
                    (
                        "Organic Apples",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        30000,
                        120,
                        30,
                        "Fresh organic apples, rich in flavor and nutrients.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Ripe Bananas",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        20000,
                        80,
                        20,
                        "Sweet and ripe bananas, perfect for a healthy snack.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Organic Carrots",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        15000,
                        90,
                        20,
                        "Crisp and fresh organic carrots, ideal for salads and snacking.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Fresh Broccoli",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        18000,
                        70,
                        20,
                        "Nutritious and fresh broccoli, perfect for a healthy diet.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Whole Wheat Flour",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        12000,
                        85,
                        25,
                        "High-quality whole wheat flour for baking and cooking.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Brown Rice",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        10000,
                        60,
                        18,
                        "Nutritious brown rice, ideal for a variety of dishes.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Organic Strawberries",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        37000,
                        110,
                        22,
                        "Juicy and sweet organic strawberries, perfect for desserts.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Sweet Potatoes",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        24000,
                        65,
                        14,
                        "Delicious sweet potatoes, great for baking or roasting.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Bell Peppers",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        27500,
                        65,
                        13,
                        "Fresh red bell peppers, ideal for salads and stir-fries.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Quinoa",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        31000,
                        80,
                        16,
                        "High-protein quinoa, perfect as a side dish or main course.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Oats",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        18000,
                        70,
                        17,
                        "Healthy oats for breakfast or baking.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Pineapples",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        36000,
                        95,
                        19,
                        "Tropical pineapples, sweet and juicy.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Zucchini",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        15000,
                        50,
                        10,
                        "Fresh zucchini, versatile for various dishes.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Fresh Milk",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        unitMeasureList[1].Id,
                        unitMeasureList[1],
                        22000,
                        90,
                        20,
                        "Pure and fresh milk, sourced from local dairy farms.",
                        userList[4].Id,
                        userList[4]
                    ),
                    // Producer 2's products
                    (
                        "Baby Carrots",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        20000,
                        50,
                        11,
                        "Sweet and tender baby carrots.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Green Beans",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        22000,
                        55,
                        13,
                        "Fresh green beans, ideal for stir-fries and sides.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Millet",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        28000,
                        50,
                        14,
                        "Nutritious millet, great for various recipes.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Buckwheat",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        34000,
                        0,
                        0,
                        "Healthy buckwheat, a great addition to your pantry.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Mangoes",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        40000,
                        85,
                        22,
                        "Sweet and juicy mangoes, perfect for smoothies and desserts.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Papayas",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        32000,
                        75,
                        18,
                        "Fresh papayas, great for fruit salads and juices.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Cherry Tomatoes",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        25000,
                        70,
                        16,
                        "Sweet cherry tomatoes, perfect for salads and snacks.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Cheddar Cheese",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        50000,
                        110,
                        25,
                        "Aged cheddar cheese with a rich, creamy flavor.",
                        userList[5].Id,
                        userList[5]
                    )
                };

                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                string seedImagePath = "D:\\UniCapProject\\sample-images";

                // Check -> delete old folder: its sub_folders and files, if any, before create the Resources directory
                if (Directory.Exists(directoryPath))
                {
                    try
                    {
                        // Delete the directory and all its contents
                        Directory.Delete(directoryPath, recursive: true);
                        Console.WriteLine($"Deleted folder and its contents: {directoryPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            $"Error deleting folder: {directoryPath}. Exception: {ex.Message}"
                        );
                    }
                }
                else
                {
                    Console.WriteLine($"Directory '{directoryPath}' does not exist.");
                }

                foreach (var product in products)
                {
                    // Create directory for each owner, if it doesn't already exist
                    string ownerImageDirectory = Path.Combine(
                        directoryPath,
                        product.OwnerId.ToString()
                    );
                    if (!Directory.Exists(ownerImageDirectory))
                    {
                        Directory.CreateDirectory(ownerImageDirectory);
                    }

                    // Copy product image to the owner's directory
                    string sourceImagePath = Path.Combine(seedImagePath, $"{product.Name}.jpg");
                    string destinationImagePath = Path.Combine(
                        ownerImageDirectory,
                        $"{product.Name}.jpg"
                    );

                    if (File.Exists(sourceImagePath))
                    {
                        File.Copy(sourceImagePath, destinationImagePath, overwrite: true);
                    }
                    else
                    {
                        // Log missing image issue instead of just printing to the console
                        Console.WriteLine(
                            $"Warning: Image for product {product.Name} not found at {sourceImagePath}."
                        );
                    }
                }

                var productList = products
                    .Select(product => new Product
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = product.CategoryId,
                        DiscountId = discountList[0].Id,
                        Category = product.Category,
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = 10,
                        UnitMeasure = product.UnitMeasure,
                        UnitMeasureId = product.UnitMeasureId,
                        Active_Status = ActiveStatus.ACTIVE,
                        Total_Rating_Value = product.TotalRatingValue,
                        Total_Rating_Quantity = product.TotalRatingQuantity,
                        Description = product.Description,
                        OwnerId = product.OwnerId,
                        Owner = product.Owner,
                        Images = [],
                        Discount = discountList[0]
                    })
                    .ToList();
                #endregion

                #region seed product_images
                var productImages = new List<(string Name, Guid ProductId, Product Product)>
                {
                    // Images for Producer 1
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
                #endregion

                #region seed orders
                TimeSpan timeSpan = new(24, 0, 0);
                var orders = new List<(Guid ProductId, Product Product)>
                {
                    (productList[0].Id, productList[0]),
                    (productList[1].Id, productList[1])
                };
                var orderList = orders
                    .Select(order => new Order
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        ProductId = order.ProductId,
                        Total_Price = 0,
                        Total_Quantity = 1,
                        EndTime = DateTime.UtcNow + timeSpan,
                        Level = 1,
                        Delivery_Status = DeliveryStatus.PENDING,
                        Product = order.Product,
                        IsShare = true,
                        IsPaid = false,
                    })
                    .ToList();

                // seed sub order
                var sub_orders = new List<(
                    Guid UserId,
                    User User,
                    Guid OrderId,
                    Order Order,
                    int Quantity,
                    double Price,
                    bool IsRating
                )>
                {
                    (
                        userList[0].Id,
                        userList[0],
                        orderList[0].Id,
                        orderList[0],
                        2,
                        productList[0].Price,
                        false
                    ),
                    (
                        userList[0].Id,
                        userList[0],
                        orderList[1].Id,
                        orderList[1],
                        2,
                        productList[1].Price,
                        true
                    ),
                    (
                        userList[1].Id,
                        userList[1],
                        orderList[1].Id,
                        orderList[1],
                        2,
                        productList[1].Price,
                        true
                    ),
                };
                foreach (var item in sub_orders)
                {
                    orderList[0].Total_Price += item.Price;
                }

                var sub_orderList = sub_orders
                    .Select(sub_order => new Sub_Order
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        UserId = sub_order.UserId,
                        OrderId = sub_order.OrderId,
                        Quantity = sub_order.Quantity,
                        Price = sub_order.Price,
                        User = sub_order.User,
                        IsRating = sub_order.IsRating
                    })
                    .ToList();
                #endregion

                #region seed comments
                var comments = new List<(
                    Guid Sub_OrderId,
                    Sub_Order Sub_Order,
                    Guid ProductId,
                    Product Product,
                    string Content,
                    int Rating
                )>
                {
                    (
                        sub_orderList[1].Id,
                        sub_orderList[1],
                        productList[1].Id,
                        productList[1],
                        "Wonderful product!",
                        5
                    ),
                    (
                        sub_orderList[2].Id,
                        sub_orderList[2],
                        productList[1].Id,
                        productList[1],
                        "Good product!",
                        3
                    )
                };
                var commentList = comments
                    .Select(comment => new Comment
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Sub_OrderId = comment.Sub_OrderId,
                        ProductId = comment.ProductId,
                        Content = comment.Content,
                        Sub_Order = comment.Sub_Order,
                        Product = comment.Product,
                        Rating = comment.Rating
                    })
                    .ToList();
                #endregion

                #region Check and seed data
                _dataContext.Unit_Measurements.AddRange(unitMeasureList);

                _dataContext.Users.AddRange(userList);
                _dataContext.Product_Categories.AddRange(productCategoryList);
                _dataContext.Products.AddRange(productList);
                _dataContext.Product_Images.AddRange(productImageList);

                _dataContext.Orders.AddRange(orderList);
                _dataContext.Sub_Orders.AddRange(sub_orderList);

                _dataContext.Discounts.AddRange(discountList);
                _dataContext.Discount_Details.AddRange(discountDetailList);
                _dataContext.Comments.AddRange(commentList);
                #endregion

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
