using CsvHelper.Configuration;
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

                var provinceList = _readerCsv.ReadCsv("Filtered_Provinces.csv", new ProvinceMap());
                var districtList = _readerCsv.ReadCsv("Filtered_Districts.csv", new DistrictMap());
                var wardList = _readerCsv.ReadCsv("Filtered_Wards.csv", new WardMap());
                #endregion

                #region seed discounts
                var discountList = new List<Discount>
                {
                    new Discount
                    {
                        Id = Guid.NewGuid(),
                        Name = "Group-Buy Discount",
                        Active_Status = ActiveStatus.ACTIVE
                    }
                };
                var discountDetails = new List<(int Level, double Amount)>
                {
                    // Group-Buy Discount
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
                        "producer1",
                        "Producer 1",
                        "producer1@gmail.com",
                        "1237894560",
                        "Saler 1",
                        UserType.SELLER
                    ),
                    (
                        "producer2",
                        "Producer 2",
                        "producer2@gmail.com",
                        "9873216540",
                        "Saler 2",
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
                        Description = user.Description
                    })
                    .ToList();
                #endregion

                #region seed product_categories
                var productCategories = new List<string>
                {
                    "Fresh Fruits and Vegetables",
                    "Dried Fruits and Vegetables",
                    "Beverages from Agricultural Products",
                    "Snacks and Baked Goods from Agricultural Products",
                    "Spices and Cooking Ingredients",
                    "Nuts and Grains",
                    "Organic Agricultural Products",
                    "Pre-packaged Salad and Fresh Cuts"
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
                    string Description,
                    Guid OwnerId,
                    User Owner
                )>
                {
                    // Producer 1's products
                    (
                        "Fresh Apples",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        30000,
                        "These fresh, organic apples are handpicked at the peak of ripeness, delivering a crisp texture and rich, juicy flavor that only nature can provide. Grown without pesticides, they are packed with essential vitamins and antioxidants, making them a wholesome choice for snacking, baking, or juicing. Their natural sweetness and satisfying crunch make them a favorite for both kids and adults.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Cherry Tomatoes",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        20000,
                        "Bursting with a delicate sweetness and a vibrant red hue, these organic cherry tomatoes are perfect for salads, cooking, or simply as a healthy snack. Carefully grown with natural methods, they are packed with lycopene, vitamins, and fiber, offering not only a delicious taste but also numerous health benefits. These tomatoes add a fresh, flavorful touch to any meal, enhancing both color and nutrition.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Dried Mango Slices",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        15000,
                        "Our dried mango slices are a delightful tropical treat, offering the luscious sweetness of ripe mangoes with a chewy texture that’s perfect for on-the-go snacking. These slices are naturally rich in vitamin C, fiber, and antioxidants, promoting skin health and immunity. With no added sugar or preservatives, they bring a pure and delicious flavor straight from the orchard, ideal for a nutritious snack or a flavorful addition to recipes.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Banana Chips",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        18000,
                        "Crispy and naturally sweet, these banana chips are a healthy and satisfying alternative to traditional snacks. Made from fresh bananas, they are carefully sliced and dehydrated to preserve their nutrients and natural flavors. Packed with potassium and fiber, they support heart health and digestive wellness, making them a guilt-free treat perfect for anytime munching or adding crunch to your favorite desserts.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "100% Orange Juice",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        12000,
                        "This 100% pure orange juice brings the sunshine of freshly picked oranges into your glass, packed with vitamin C, folate, and antioxidants. It has a smooth, refreshing taste with a hint of natural sweetness, making it perfect for breakfast or as an energizing refreshment throughout the day. With no added sugars or preservatives, it’s a naturally healthy choice for the whole family.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Aloe Vera Drink",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        10000,
                        "Our aloe vera drink offers a refreshing way to enjoy the hydrating and soothing benefits of aloe. Naturally rich in vitamins, minerals, and amino acids, it promotes digestive health and provides a calming effect on the body. The subtle, mildly sweet taste makes it a delightful, guilt-free beverage for any time of the day, with added hydration benefits for healthy skin and well-being.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Rice Crackers",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        37000,
                        "Light, crunchy, and subtly seasoned, these rice crackers make an excellent snack that pairs well with dips, cheese, or just on their own. Made from high-quality rice, they are gluten-free and baked to perfection, offering a delightful balance of flavor and crunch. Ideal for a healthy, on-the-go snack or as a sophisticated party appetizer, they provide a satisfying crunch without added guilt.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Nut & Seed Energy Bars",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        24000,
                        "Packed with wholesome nuts and seeds, these energy bars are a nutrient-dense snack for sustained energy. With a combination of protein, fiber, and healthy fats, they are designed to fuel your day naturally, without artificial ingredients. Perfect for pre- or post-workout, or as a midday pick-me-up, each bar provides a delicious and nourishing boost that’s both filling and flavorful.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Garlic Powder",
                        productCategoryList[4].Id,
                        productCategoryList[4],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        27500,
                        "A must-have in every kitchen, this high-quality garlic powder brings robust flavor and aroma to any dish. Made from finely ground, fresh garlic bulbs, it’s a convenient way to add depth and complexity to soups, sauces, marinades, and rubs. Full of natural garlic’s health benefits, it’s rich in antioxidants and supports immune health, making it a versatile and essential spice in your pantry.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Turmeric Powder",
                        productCategoryList[4].Id,
                        productCategoryList[4],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        31000,
                        "This vibrant turmeric powder is packed with curcumin, known for its powerful anti-inflammatory and antioxidant properties. Sourced from high-quality turmeric roots, it adds a warm, earthy flavor and a golden color to curries, soups, and smoothies. An essential spice in many cuisines, it’s also renowned for supporting joint health and immune function.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Raw Cashews",
                        productCategoryList[5].Id,
                        productCategoryList[5],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        18000,
                        "These raw cashews are naturally delicious and packed with essential nutrients. Rich in healthy fats, protein, and magnesium, they make a wholesome snack on their own or can be used as a creamy addition to recipes. Perfect for vegan and paleo diets, they add a buttery, slightly sweet flavor to both sweet and savory dishes.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Macadamia Nuts",
                        productCategoryList[5].Id,
                        productCategoryList[5],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        36000,
                        "Known for their creamy texture and rich, buttery taste, these macadamia nuts are a true gourmet delight. Grown naturally, they are full of monounsaturated fats and are a good source of antioxidants, which help in supporting heart health. Enjoy them as a luxurious snack or incorporate them into baked goods for a rich flavor profile.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Organic Brown Rice",
                        productCategoryList[6].Id,
                        productCategoryList[6],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        15000,
                        "Our organic brown rice is whole grain, nutrient-dense, and perfect for a healthy, balanced diet. It has a nutty flavor and chewy texture that complements various dishes, from stir-fries to grain bowls. Packed with fiber, vitamins, and minerals, it’s a staple for those looking to enhance their meals with whole, natural goodness.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Organic Tomato Sauce",
                        productCategoryList[6].Id,
                        productCategoryList[6],
                        unitMeasureList[1].Id,
                        unitMeasureList[1],
                        22000,
                        "This rich, organic tomato sauce is made from sun-ripened tomatoes, providing a natural and delicious foundation for pasta, pizza, and other culinary creations. With no artificial additives, it’s a flavorful and healthy choice, offering the fresh taste of tomatoes while adding depth and brightness to any dish.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Garden Salad Mix",
                        productCategoryList[7].Id,
                        productCategoryList[7],
                        unitMeasureList[1].Id,
                        unitMeasureList[1],
                        22000,
                        "A vibrant mix of fresh, crisp greens, perfect for creating a wholesome, nutrient-packed salad. This garden salad mix offers a balance of flavors and textures, bringing a burst of freshness to any meal. It’s an ideal base for a light, refreshing salad or a healthy addition to sandwiches and wraps.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Sliced Bell Peppers",
                        productCategoryList[7].Id,
                        productCategoryList[7],
                        unitMeasureList[1].Id,
                        unitMeasureList[1],
                        25000,
                        "Colorful, crunchy, and packed with vitamin C, these sliced bell peppers are perfect for salads, stir-fries, or snacks. They add a fresh, sweet flavor and a vibrant touch to any dish, while also providing antioxidants and fiber. Great for those looking to add a healthy, natural burst of color and nutrients to their meals.",
                        userList[4].Id,
                        userList[4]
                    ),
                    // Producer 2's products
                    (
                        "Bananas",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        30000,
                        "Sweet and ripe bananas, rich in potassium and fiber, offering a natural energy boost. They are perfect for snacking, adding to smoothies, or incorporating into baked goods like banana bread, making them a versatile and healthy addition to any diet.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Mixed Leafy Greens",
                        productCategoryList[0].Id,
                        productCategoryList[0],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        20000,
                        "A fresh and vibrant mix of nutrient-packed leafy greens, including spinach, kale, and arugula, rich in vitamins, antioxidants, and fiber. Ideal for creating healthy salads, adding to smoothies, or incorporating into soups and wraps, these greens support overall wellness and digestive health.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Sweet Potato Crisps",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        15000,
                        "Crisp, lightly salted sweet potato chips, made from fresh, high-quality sweet potatoes. These chips are a healthier alternative to regular potato chips, packed with vitamins A and C and a great source of dietary fiber. Perfect for satisfying your crunchy snack cravings while maintaining a nutritious diet.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Dried Pineapple Rings",
                        productCategoryList[1].Id,
                        productCategoryList[1],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        18000,
                        "Delicious and naturally sweet dried pineapple rings, carefully dehydrated to preserve their tropical flavor. Rich in vitamin C, antioxidants, and digestive enzymes, these pineapple rings are a perfect on-the-go snack, or can be used in baking, smoothies, or as a topping for cereals and salads.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Green Smoothie Blend",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        12000,
                        "A nutritious blend of green vegetables like spinach, kale, and other leafy greens, specifically designed for making healthy smoothies. Packed with essential vitamins, minerals, and antioxidants, this blend supports detoxification, boosts energy levels, and aids in digestion, helping you maintain a balanced and healthy lifestyle.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Herbal Tea Mix",
                        productCategoryList[2].Id,
                        productCategoryList[2],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        10000,
                        "A soothing and aromatic blend of natural herbs like chamomile, peppermint, and lemongrass, perfect for winding down after a long day. This herbal tea mix offers calming properties, promotes relaxation, and aids in digestion, making it an ideal choice for those seeking natural remedies to improve overall wellness and stress relief.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Sweet Corn Puffs",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        37000,
                        "Light and airy sweet corn puffs, made from high-quality corn that delivers a crunchy texture and a delicious, mildly sweet flavor. These puffs are a low-calorie, gluten-free snack that can be enjoyed by both kids and adults. They're perfect for quick munching, in lunchboxes, or as a side dish to complement meals.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Potato Chips",
                        productCategoryList[3].Id,
                        productCategoryList[3],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        24000,
                        "Crispy, golden-brown potato chips made from premium potatoes, lightly salted to bring out their natural flavor. These chips are a classic, satisfying snack that pairs well with dips, sandwiches, or as a standalone treat. They offer a crunchy indulgence for those who love simple, savory flavors.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Fermented Chili Sauce",
                        productCategoryList[4].Id,
                        productCategoryList[4],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        27500,
                        "A spicy, tangy fermented chili sauce made from fresh, ripe chilies and garlic, offering bold, complex flavors with just the right amount of heat. Perfect for drizzling on grilled meats, mixing into stir-fries, or as a dipping sauce for appetizers, this chili sauce adds a fiery kick to any dish.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Ginger Paste",
                        productCategoryList[4].Id,
                        productCategoryList[4],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        31000,
                        "Fresh, aromatic ginger paste, a convenient alternative to grating fresh ginger. This paste is ideal for adding depth and spice to your dishes, whether in soups, stir-fries, marinades, or curries. Rich in gingerol, it offers anti-inflammatory and digestive benefits, making it a staple in many kitchens.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Rolled Oats",
                        productCategoryList[5].Id,
                        productCategoryList[5],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        18000,
                        "Nutritious rolled oats, perfect for preparing a hearty and filling breakfast. Rich in fiber, antioxidants, and vitamins, these oats help regulate blood sugar levels, support heart health, and promote digestive well-being. They're ideal for making oatmeal, granola, or adding to baking recipes for added nutrition.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Chia Seeds",
                        productCategoryList[5].Id,
                        productCategoryList[5],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        36000,
                        "Tiny but mighty chia seeds, packed with omega-3 fatty acids, fiber, protein, and essential minerals. These seeds are great for adding to smoothies, yogurt, salads, or baking. They absorb liquid, creating a gel-like texture, making them an ideal ingredient for chia pudding or as a thickener for sauces and soups.",
                        userList[4].Id,
                        userList[4]
                    ),
                    (
                        "Organic Mixed Beans",
                        productCategoryList[6].Id,
                        productCategoryList[6],
                        unitMeasureList[0].Id,
                        unitMeasureList[0],
                        15000,
                        "A mix of organic beans including kidney, black, and pinto beans, providing a great source of plant-based protein, fiber, and essential vitamins. Perfect for adding to soups, stews, salads, or making homemade bean burgers, these mixed beans offer versatile, nutritious options for a variety of dishes.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Organic Applesauce",
                        productCategoryList[6].Id,
                        productCategoryList[6],
                        unitMeasureList[1].Id,
                        unitMeasureList[1],
                        22000,
                        "Smooth and sweet organic applesauce made from fresh, locally grown apples. This applesauce contains no added sugars or preservatives, offering a pure and natural flavor. Ideal as a snack, a topping for oatmeal, or as an ingredient in baking recipes, it's a wholesome, guilt-free treat for all ages.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Carrot Sticks",
                        productCategoryList[7].Id,
                        productCategoryList[7],
                        unitMeasureList[1].Id,
                        unitMeasureList[1],
                        22000,
                        "Fresh, crunchy carrot sticks, rich in vitamins A and C, perfect for a healthy snack. These carrot sticks are not only great for munching on their own but also pair wonderfully with dips, or can be added to salads and stir-fries to boost your vegetable intake in a tasty way.",
                        userList[5].Id,
                        userList[5]
                    ),
                    (
                        "Caesar Salad Kit",
                        productCategoryList[7].Id,
                        productCategoryList[7],
                        unitMeasureList[1].Id,
                        unitMeasureList[1],
                        22000,
                        "A complete Caesar salad kit containing crisp Romaine lettuce, savory croutons, and a rich, creamy Caesar dressing. This kit makes preparing a delicious and satisfying salad quick and easy, offering a balanced mix of fresh, high-quality ingredients that are perfect for a light lunch or a hearty side dish.",
                        userList[5].Id,
                        userList[5]
                    ),
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
                        UnitMeasureId = product.UnitMeasureId,
                        UnitMeasure = product.UnitMeasure,
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
                        Total_Price = 0,
                        EndTime = DateTime.UtcNow + timeSpan,
                        ProvinceId = "74",
                        Level = 0,
                        Delivery_Status = DeliveryStatus.PENDING,
                        IsShare = true,
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
                #endregion

                #region seed feedbacks
                var feedbacks = new List<(
                    Guid Item_OrderId,
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
                foreach (var item in feedbacks)
                {
                    item.Product.Total_Rating_Value += item.Rating;
                    item.Product.Total_Rating_Quantity += 1;
                }
                var feedbackList = feedbacks
                    .Select(feedback => new Feedback
                    {
                        Id = Guid.NewGuid(),
                        Created_At = DateTime.UtcNow,
                        Modified_At = DateTime.UtcNow,
                        Item_OrderId = feedback.Item_OrderId,
                        ProductId = feedback.ProductId,
                        Content = feedback.Content,
                        Sub_Order = feedback.Sub_Order,
                        Product = feedback.Product,
                        Rating = feedback.Rating
                    })
                    .ToList();
                #endregion

                #region Check and seed data
                _dataContext.Unit_Measurements.AddRange(unitMeasureList);
                _dataContext.Provinces.AddRange(provinceList);
                _dataContext.Districts.AddRange(districtList);
                _dataContext.Wards.AddRange(wardList);

                _dataContext.Users.AddRange(userList);
                _dataContext.Product_Categories.AddRange(productCategoryList);
                _dataContext.Products.AddRange(productList);

                // _dataContext.Product_Images.AddRange(productImageList); -- wait

                _dataContext.Orders.AddRange(orderList);
                _dataContext.Sub_Orders.AddRange(sub_orderList);

                _dataContext.Discounts.AddRange(discountList);
                _dataContext.Discount_Details.AddRange(discountDetailList);
                _dataContext.Feedbacks.AddRange(feedbackList);
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
