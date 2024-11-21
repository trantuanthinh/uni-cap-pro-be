using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
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
        private readonly ReaderCsv _readerCsv;

        public DatabaseSeeder(DataContext dataContext, ReaderCsv readerCsv)
        {
            _dataContext = dataContext;
            _readerCsv = readerCsv;
        }

        public class ProvinceMap : ClassMap<Province>
        {
            public ProvinceMap()
            {
                // Map from CSV column to C# property
                Map(m => m.Id).Name("Id").TypeConverterOption.NullValues("");
                Map(m => m.Name).Name("Name");
            }
        }

        public class DistrictMap : ClassMap<District>
        {
            public DistrictMap()
            {
                // Map from CSV column to C# property
                Map(m => m.Id).Name("Id").TypeConverterOption.NullValues("");
                Map(m => m.Name).Name("Name");
                Map(m => m.ProvinceId).Name("ProvinceId");
            }
        }

        public class WardMap : ClassMap<Ward>
        {
            public WardMap()
            {
                // Map from CSV column to C# property
                Map(m => m.Id).Name("Id").TypeConverterOption.NullValues("");
                Map(m => m.Name).Name("Name");
                Map(m => m.DistrictId).Name("DistrictId");
            }
        }

        public void SeedDataContext()
        {
            try
            {
                // Ensure the database is created
                _dataContext.Database.EnsureCreated();

                bool hasProvinces = _dataContext.Provinces.Any();
                bool hasDistricts = _dataContext.Districts.Any();
                bool hasWards = _dataContext.Wards.Any();

                if (hasProvinces || hasDistricts || hasWards)
                {
                    return;
                }
                CreateTriggers(_dataContext);

                #region seed settings-data
                var provinceList = _readerCsv.ReadCsv("Filtered_Provinces.csv", new ProvinceMap());
                var districtList = _readerCsv.ReadCsv("Filtered_Districts.csv", new DistrictMap());
                var wardList = _readerCsv.ReadCsv("Filtered_Wards.csv", new WardMap());
                #endregion

                #region seed discounts
                var discounts = new List<string> { "Group-Buy Discount", "Tiered Discount" };
                var discountList = discounts
                    .Select(discount => new Discount
                    {
                        Id = Guid.NewGuid(),
                        Name = discount,
                        Created_At = DateTime.Now,
                        Modified_At = DateTime.Now
                    })
                    .ToList();

                var discountDetails = new List<(Guid discountId, int level, double amount)>
                {
                    // Group-Buy Discount
                    (discountList[0].Id, 1, 0),
                    (discountList[0].Id, 2, 0.1),
                    (discountList[0].Id, 3, 0.15),
                    (discountList[0].Id, 4, 0.2),
                    (discountList[0].Id, 5, 0.25),
                    (discountList[0].Id, 6, 0.4),
                    // Tiered Discount
                    (discountList[1].Id, 1, 0),
                    (discountList[1].Id, 2, 0.05),
                    (discountList[1].Id, 3, 0.07),
                    (discountList[1].Id, 4, 0.1),
                };

                var discountDetailList = discountDetails
                    .Select(discount_detail => new Discount_Detail
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        DiscountId = discount_detail.discountId,
                        Level = discount_detail.level,
                        Amount = discount_detail.amount,
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
                        "conan246817@gmail.com",
                        "0395279915",
                        "Tran Tuan Thinh",
                        UserType.BUYER
                    ),
                    (
                        "thaohoang",
                        "Hoang Thi Thanh Thao",
                        "thaoh1717@gmail.com",
                        "0327858682",
                        "ThaoHoang1717",
                        UserType.BUYER
                    ),
                    (
                        "company1",
                        "Company 1",
                        "company1@gmail.com",
                        "1234567890",
                        "Buyer 1",
                        UserType.BUYER
                    ),
                    (
                        "company2",
                        "Company 2",
                        "company2@gmail.com",
                        "9876543210",
                        "Buyer 2",
                        UserType.BUYER
                    ),
                    (
                        "seller1",
                        "Seller 1",
                        "seller1@gmail.com",
                        "1237894560",
                        "Seller 1",
                        UserType.SELLER
                    ),
                    (
                        "seller2",
                        "Seller 2",
                        "seller2@gmail.com",
                        "9873216540",
                        "Seller 2",
                        UserType.SELLER
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
                        Description = user.Description,
                        Address = "Nguyen Van Tiet",
                        ProvinceId = provinceList[0].Id,
                        Province = provinceList[0],
                        DistrictId = districtList[0].Id,
                        District = districtList[0],
                        WardId = wardList[0].Id,
                        Ward = wardList[0],
                    })
                    .ToList();
                #endregion

                #region seed product_categories
                var mainCategories = new List<string>
                {
                    "Cooking Ingredients",
                    "Groceries and Food Staples",
                    "Personal Care Products",
                    "Snacks and Confectionery",
                };
                var mainCategoryList = mainCategories
                    .Select(category => new Product_Main_Category
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Total_Product = 0,
                        Name = category
                    })
                    .ToList();

                var productCategories = new List<(
                    Guid mainId,
                    Product_Main_Category mainCategory,
                    string Name
                )>
                {
                    // Cooking Ingredients
                    (mainCategoryList[0].Id, mainCategoryList[0], "Baking Supplies"),
                    (mainCategoryList[0].Id, mainCategoryList[0], "Cooking Spices"),
                    (mainCategoryList[0].Id, mainCategoryList[0], "Pasta and Noodles"),
                    (mainCategoryList[0].Id, mainCategoryList[0], "Sauces and Condiments"),
                    // Groceries and Food Staples
                    (mainCategoryList[1].Id, mainCategoryList[1], "Canned Goods"),
                    (mainCategoryList[1].Id, mainCategoryList[1], "Cooking Oil"),
                    (mainCategoryList[1].Id, mainCategoryList[1], "Flour"),
                    (mainCategoryList[1].Id, mainCategoryList[1], "Rice"),
                    // Personal Care Products
                    (
                        mainCategoryList[2].Id,
                        mainCategoryList[2],
                        "Deodorants and Antiperspirants"
                    ),
                    (mainCategoryList[2].Id, mainCategoryList[2], "Shamppos and Conditioners"),
                    (mainCategoryList[2].Id, mainCategoryList[2], "Soaps and Body Washes"),
                    (mainCategoryList[2].Id, mainCategoryList[2], "Toothpaste and Oral Care Items"),
                    // Snacks and Confectionery
                    (mainCategoryList[3].Id, mainCategoryList[3], "Dried Vegetables"),
                    (mainCategoryList[3].Id, mainCategoryList[3], "Dried Fruits"),
                    (mainCategoryList[3].Id, mainCategoryList[3], "Nuts and Seeds"),
                };

                var productCategoryList = productCategories
                    .Select(category => new Product_Category
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Main_CategoryId = category.mainId,
                        Total_Product = 0,
                        Name = category.Name,
                    })
                    .ToList();
                #endregion

                #region seed products for each producer
                var products = new List<(
                    string Name,
                    Guid CategoryId,
                    Product_Category Category,
                    double Price,
                    string Description,
                    Guid OwnerId,
                    User Owner
                )>
                {
                    // Producer 1's products
                    (
                        "Cornstarch",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        30000,
                        "Cornstarch is a fine powder made from the endosperm of corn kernels. It is commonly used as a thickening agent in cooking and baking, providing a smooth texture for sauces, gravies, and soups. It is also used in some cosmetic and pharmaceutical applications.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Grain Powder",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        20000,
                        "Grain powder is a finely ground product derived from various types of grains, offering a rich, earthy flavor. It can be used in baking, cooking, or as an ingredient in smoothies and shakes for a healthy, fiber-rich boost.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Cinnamon",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        15000,
                        "Cinnamon is a spice made from the inner bark of Cinnamomum trees. It is known for its warm, sweet flavor and is commonly used in both sweet and savory dishes, including baked goods, teas, and curries. It also offers various health benefits.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Egg Noodles",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        18000,
                        "Egg noodles are a type of pasta made from eggs and flour. Known for their soft and chewy texture, they are commonly used in soups, stir-fries, and various Asian dishes.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Rice Vermicelli",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        12000,
                        "Rice vermicelli is a thin noodle made from rice flour. It is commonly used in Asian cuisines, particularly in soups, spring rolls, and stir-fries. It has a light texture and absorbs flavors well.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Oyster Sauce",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        10000,
                        "Oyster sauce is a savory, dark sauce made from oyster extracts, sugar, salt, and sometimes other seasonings. It is commonly used in Chinese cooking to enhance the flavor of stir-fries, vegetables, and meat dishes.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Soy Sauce",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        37000,
                        "Soy sauce is a fermented sauce made from soybeans, wheat, salt, and water. It has a salty, umami-rich flavor and is widely used in Asian cooking as a seasoning for dishes such as stir-fries, sushi, and soups.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Canned Sweet Corn",
                        productCategoryList[4].Id,
                        productCategoryList[4],
                        24000,
                        "Canned sweet corn is a convenient and ready-to-eat product made from sweet corn kernels. It can be used in a variety of dishes, including salads, soups, and side dishes, providing a natural sweetness and nutritional value.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Coconut Oil",
                        productCategoryList[5].Id,
                        productCategoryList[5],
                        27500,
                        "Coconut oil is a natural oil extracted from the flesh of coconuts. It is commonly used in cooking, baking, and skin care products due to its moisturizing and antibacterial properties.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Corn Flour",
                        productCategoryList[6].Id,
                        productCategoryList[6],
                        31000,
                        "Corn flour is a finely ground flour made from corn kernels. It is used in baking, as a thickening agent in sauces and soups, and to create a crisp texture in frying applications.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Rice Flour",
                        productCategoryList[6].Id,
                        productCategoryList[6],
                        18000,
                        "Rice flour is a fine flour made from ground rice. It is commonly used in gluten-free baking, as a thickener for sauces, and in Asian dishes such as rice cakes and dumplings.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Basmati Rice",
                        productCategoryList[7].Id,
                        productCategoryList[7],
                        36000,
                        "Basmati rice is a fragrant, long-grain rice variety often used in Indian and Middle Eastern cuisines. Known for its delicate aroma and fluffy texture, it is ideal for pilafs, curries, and side dishes.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Lavender & Aloe Vera Natural Deodorant",
                        productCategoryList[8].Id,
                        productCategoryList[8],
                        15000,
                        "Lavender & Aloe Vera Natural Deodorant combines the soothing properties of aloe vera with the calming scent of lavender. This natural deodorant offers long-lasting freshness without synthetic fragrances or harsh chemicals.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Aloe Vera Shampoo",
                        productCategoryList[9].Id,
                        productCategoryList[9],
                        22000,
                        "Aloe vera shampoo nourishes the scalp and hair with the natural soothing properties of aloe vera. It provides hydration and helps strengthen hair, leaving it soft, shiny, and manageable.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Coconut Oil Soap",
                        productCategoryList[10].Id,
                        productCategoryList[10],
                        22000,
                        "Coconut oil soap is a natural soap made from coconut oil, known for its moisturizing and skin-softening properties. It gently cleanses the skin, leaving it feeling soft, smooth, and hydrated.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Coconut Vanilla",
                        productCategoryList[10].Id,
                        productCategoryList[10],
                        25000,
                        "Coconut vanilla soap combines the rich, creamy scent of coconut with the sweet, aromatic fragrance of vanilla. This soap nourishes the skin while offering a luxurious, aromatic experience.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Aloe Vera Toothpaste",
                        productCategoryList[11].Id,
                        productCategoryList[11],
                        25000,
                        "Aloe vera toothpaste provides a gentle yet effective clean while soothing the gums with aloe vera. This natural toothpaste is designed to protect enamel and promote healthy gums without harsh chemicals.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Bamboo Charcoal Toothpaste",
                        productCategoryList[11].Id,
                        productCategoryList[11],
                        25000,
                        "Bamboo charcoal toothpaste uses the natural purifying properties of bamboo charcoal to help remove impurities, brighten teeth, and promote healthy gums. Its gentle formula is ideal for sensitive teeth and gums.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Dried Apricots",
                        productCategoryList[12].Id,
                        productCategoryList[12],
                        25000,
                        "Dried apricots are naturally sweet and rich in vitamins and fiber. They make for a healthy snack and are also great in baked goods, salads, or as a topping for cereals and yogurt.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Dried Carrot Chips",
                        productCategoryList[13].Id,
                        productCategoryList[13],
                        20000,
                        "Dried carrot chips are a crunchy, nutritious snack made from dehydrated carrots. They are packed with vitamins and are perfect for a healthy snack or as an addition to salads and dishes.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Almonds",
                        productCategoryList[14].Id,
                        productCategoryList[14],
                        35000,
                        "Almonds are a rich source of vitamins, minerals, and healthy fats. Great for snacking, baking, or adding to meals.",
                        userList[4].Id,
                        userList[4]
                    ),
                    // Producer 2's products
                    (
                        "Oatmeal",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        30000,
                        "Oatmeal is a versatile breakfast option made from ground oats, providing a warm and hearty meal. It is often used as a base for toppings like fruits, nuts, or sweeteners, and offers a good source of fiber and nutrients.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Garlic",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        20000,
                        "Garlic is a pungent herb commonly used in cooking for its strong flavor and aroma. It has various health benefits, including being known for its antioxidant and antimicrobial properties.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Spaghetti",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        15000,
                        "Spaghetti is a popular type of pasta made from durum wheat. It is a staple in Italian cuisine, commonly served with a variety of sauces, such as marinara, meat sauce, or pesto.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Tomato Ketchup",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        18000,
                        "Tomato ketchup is a sweet and tangy condiment made from tomatoes, vinegar, sugar, and spices. It is a popular addition to burgers, fries, and other fast food items.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Canned Tomato Puree",
                        productCategoryList[4].Id,
                        productCategoryList[4],
                        12000,
                        "Canned tomato puree is a smooth, thick liquid made from fresh tomatoes. It's commonly used in soups, sauces, and stews as a base ingredient for rich, savory flavors.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Olive Oil",
                        productCategoryList[4].Id,
                        productCategoryList[4],
                        12000,
                        "Olive oil is a healthy, monounsaturated fat used in cooking, frying, and as a dressing. It's known for its rich flavor and potential health benefits, including heart health.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Vegetable Oil",
                        productCategoryList[5].Id,
                        productCategoryList[5],
                        12000,
                        "Vegetable oil is a commonly used cooking oil extracted from plants. It has a neutral flavor and is often used for frying, sautéing, and baking.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Whole Wheat Flour",
                        productCategoryList[6].Id,
                        productCategoryList[6],
                        37000,
                        "Whole wheat flour is made from the entire grain of wheat, offering more fiber and nutrients compared to refined flour. It is used in baking bread, pancakes, and other whole grain products.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Sticky Rice",
                        productCategoryList[7].Id,
                        productCategoryList[7],
                        24000,
                        "Sticky rice, also known as glutinous rice, is a type of rice that becomes sticky when cooked. It is commonly used in Asian cuisines, especially in dishes like sushi and sticky rice with mango.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Natural Coconut Oil Deodorant",
                        productCategoryList[8].Id,
                        productCategoryList[8],
                        27500,
                        "Natural coconut oil deodorant is an eco-friendly alternative to traditional deodorants. It uses the moisturizing properties of coconut oil to keep you feeling fresh and odor-free without harsh chemicals.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Coconut Oil Conditioner",
                        productCategoryList[9].Id,
                        productCategoryList[9],
                        31000,
                        "Coconut oil conditioner nourishes and hydrates the hair with the natural benefits of coconut oil. It helps in repairing dry or damaged hair while leaving it soft and shiny.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Matcha Soap",
                        productCategoryList[10].Id,
                        productCategoryList[10],
                        36000,
                        "Matcha soap is made with green tea powder, offering antioxidant properties that help nourish and cleanse the skin. It provides a refreshing scent and is great for exfoliating.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Herbal Toothpaste",
                        productCategoryList[11].Id,
                        productCategoryList[11],
                        15000,
                        "Herbal toothpaste is made with natural ingredients like neem, mint, and clove. It helps in oral care by freshening breath, cleaning teeth, and offering antibacterial properties.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Dried Mango",
                        productCategoryList[12].Id,
                        productCategoryList[12],
                        22000,
                        "Dried mango is a sweet and chewy snack made from mangoes that have been dehydrated. It's a convenient, healthy snack with a rich flavor and is packed with vitamins and antioxidants.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Dried Tomato Slices",
                        productCategoryList[13].Id,
                        productCategoryList[13],
                        22000,
                        "Dried tomato slices are a flavorful and convenient ingredient for cooking. They can be rehydrated or added directly to soups, sauces, and salads for a tangy burst of flavor.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Chia Seeds",
                        productCategoryList[14].Id,
                        productCategoryList[14],
                        25000,
                        "Chia seeds are tiny, nutrient-dense seeds that are packed with omega-3 fatty acids, fiber, and antioxidants. They are great for adding to smoothies, yogurt, or baked goods.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "FlaxSeed",
                        productCategoryList[14].Id,
                        productCategoryList[14],
                        25000,
                        "Flaxseeds are a rich source of fiber, omega-3 fatty acids, and lignans. They can be used in smoothies, baked goods, or as a topping for yogurt or cereal.",
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
                        Category = product.Category,
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = 10,
                        Active_Status = ActiveStatus.ACTIVE,
                        Total_Rating_Value = 0,
                        Total_Rating_Quantity = 0,
                        Total_Sold_Quantity = 0,
                        Description = product.Description,
                        OwnerId = product.OwnerId,
                        Owner = product.Owner,
                        Images = [],
                    })
                    .ToList();

                foreach (var product in productList)
                {
                    product.Category.Total_Product += product.Quantity;
                    foreach (var mainCategory in mainCategoryList)
                    {
                        if (mainCategory.Id == product.Category.Main_CategoryId)
                        {
                            mainCategory.Total_Product += product.Category.Total_Product;
                        }
                    }
                }
                #endregion

                #region seed product_images
                var productImages = new List<(string Name, Guid ProductId, Product Product)>();

                for (int i = 0; i < productList.Count; i++)
                {
                    productImages.Add(
                        (productList[i].Name + ".jpg", productList[i].Id, productList[i])
                    );
                }

                var productImageList = productImages
                    .Select(image => new Product_Image
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Name = image.Name,
                        ProductId = image.ProductId,
                        Product = image.Product
                    })
                    .ToList();
                #endregion

                #region seed orders
                TimeSpan timeSpan = new(24, 0, 0);

                var orders = new List<(Guid ProductId, Product Product, Guid OwnerId, User Owner)>
                {
                    (
                        productList[0].Id,
                        productList[0],
                        productList[0].OwnerId,
                        productList[0].Owner
                    ),
                    (
                        productList[1].Id,
                        productList[1],
                        productList[1].OwnerId,
                        productList[1].Owner
                    )
                };

                var orderList = orders
                    .Select(order => new Order
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Total_Price = 0,
                        EndTime = DateTime.UtcNow + timeSpan,
                        StoreId = order.OwnerId,
                        Store = order.Owner,
                        DistrictId = districtList[0].Id,
                        District = districtList[0],
                        Level = 0,
                        Delivery_Status = DeliveryStatus.PENDING,
                        IsShare = true,
                        IsActive = true,
                    })
                    .ToList();

                // seed sub_orders
                var sub_orders = new List<(Guid UserId, User User, Guid OrderId, Order Order)>
                {
                    (userList[0].Id, userList[0], orderList[0].Id, orderList[0]),
                    (userList[0].Id, userList[0], orderList[1].Id, orderList[1]),
                    (userList[1].Id, userList[1], orderList[1].Id, orderList[1]),
                };

                foreach (var item in sub_orders)
                {
                    item.Order.Level += 1;
                }

                var sub_orderList = sub_orders
                    .Select(sub_order => new Sub_Order
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        UserId = sub_order.UserId,
                        OrderId = sub_order.OrderId,
                        Total_Price = 0,
                        User = sub_order.User,
                        IsPaid = false,
                    })
                    .ToList();

                // seed item_orders
                var item_orders = new List<(
                    Guid Sub_OrderId,
                    Sub_Order Sub_Order,
                    Guid ProductId,
                    Product Product,
                    int Quantity
                )>
                {
                    (sub_orderList[0].Id, sub_orderList[0], productList[0].Id, productList[0], 5),
                    (sub_orderList[1].Id, sub_orderList[1], productList[1].Id, productList[1], 20),
                    (sub_orderList[2].Id, sub_orderList[2], productList[1].Id, productList[1], 10),
                    (sub_orderList[2].Id, sub_orderList[2], productList[0].Id, productList[0], 17),
                };

                foreach (var item in item_orders)
                {
                    item.Product.Total_Sold_Quantity += item.Quantity;
                    item.Sub_Order.Total_Price += item.Product.Price;
                }

                var item_orderList = item_orders
                    .Select(item_order => new Item_Order
                    {
                        Id = Guid.NewGuid(),
                        Sub_OrderId = item_order.Sub_OrderId,
                        ProductId = item_order.ProductId,
                        Product = item_order.Product,
                        Quantity = item_order.Quantity,
                        IsRating = false,
                    })
                    .ToList();
                #endregion

                #region seed feedbacks
                var feedbacks = new List<(
                    Guid UserId,
                    User User,
                    Guid Item_OrderId,
                    Item_Order Item_Order,
                    Guid ProductId,
                    Product Product,
                    string Content,
                    int Rating
                )>
                {
                    (
                        userList[0].Id,
                        userList[0],
                        item_orderList[1].Id,
                        item_orderList[1],
                        productList[1].Id,
                        productList[1],
                        "Wonderful product!",
                        5
                    ),
                    (
                        userList[1].Id,
                        userList[1],
                        item_orderList[2].Id,
                        item_orderList[2],
                        productList[1].Id,
                        productList[1],
                        "Good product!",
                        3
                    )
                };
                foreach (var item in feedbacks)
                {
                    item.Product.Total_Rating_Value += item.Rating;
                    item.Product.Total_Rating_Quantity += 1;
                    item.Item_Order.IsRating = true;
                }

                var feedbackList = feedbacks
                    .Select(feedback => new Feedback
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        UserId = feedback.UserId,
                        User = feedback.User,
                        Item_OrderId = feedback.Item_OrderId,
                        ProductId = feedback.ProductId,
                        Content = feedback.Content,
                        Item_Order = feedback.Item_Order,
                        Product = feedback.Product,
                        Rating = feedback.Rating
                    })
                    .ToList();

                #endregion

                #region Check and seed data
                _dataContext.Provinces.AddRange(provinceList);
                _dataContext.Districts.AddRange(districtList);
                _dataContext.Wards.AddRange(wardList);

                _dataContext.Users.AddRange(userList);
                _dataContext.Product_Main_Categories.AddRange(mainCategoryList);
                _dataContext.Product_Categories.AddRange(productCategoryList);
                _dataContext.Products.AddRange(productList);
                _dataContext.Product_Images.AddRange(productImageList);

                _dataContext.Orders.AddRange(orderList);
                _dataContext.Sub_Orders.AddRange(sub_orderList);
                _dataContext.Item_Orders.AddRange(item_orderList);

                _dataContext.Discounts.AddRange(discountList);
                _dataContext.Discount_Details.AddRange(discountDetailList);
                _dataContext.Feedbacks.AddRange(feedbackList);
                #endregion

                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        public void CreateTriggers(DataContext context)
        {
            try
            {
                var triggerQueries = new[]
                {
                    // After Insert
                    TriggerQuery.AfterProductsInsert(),
                    TriggerQuery.AfterProductCategoriesInsert(),
                    TriggerQuery.AfterItemOrderInsert(),
                    TriggerQuery.AfterSubOrderInsert(),
                    TriggerQuery.AfterFeedbacksInsert(),
                    // After Update
                    TriggerQuery.AfterProductsUpdate(),
                    TriggerQuery.AfterProductCategoriesUpdate(),
                    TriggerQuery.AfterItemOrdersUpdate(),
                    TriggerQuery.AfterSubOrdersUpdate(),
                    TriggerQuery.AfterFeedbacksUpdate()
                };

                foreach (var triggerQuery in triggerQueries)
                {
                    context.Database.ExecuteSqlRaw(triggerQuery);
                }
            }
            catch (Exception ex)
            {
                // Log the error or handle it according to your needs
                Console.WriteLine($"Error creating triggers: {ex.Message}");
            }
        }
    }
}
