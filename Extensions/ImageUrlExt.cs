// using uni_cap_pro_be.Models;

// namespace uni_cap_pro_be.Extensions
// {
//     public static class ImageUrlExtension
//     {
//         private static string _host = "http://localhost:5130/api/product_images/";

//         public static async Task<List<string>> GetImagesURLByProductAsync(this Product product)
//         {
//             // Ensure product has images
//             if (product.Images == null || !product.Images.Any())
//             {
//                 return new List<string>();
//             }

//             var directoryPath = Path.Combine(
//                 Directory.GetCurrentDirectory(),
//                 $"Resources\\{product.OwnerId}"
//             );

//             // Check if the directory exists
//             if (!Directory.Exists(directoryPath))
//             {
//                 return new List<string>(); // Return an empty list if the directory does not exist
//             }

//             var imageUrls = new List<string>();

//             try
//             {
//                 foreach (var name in product.Images)
//                 {
//                     var filePath = Path.Combine(directoryPath, name.ToString());
//                     if (File.Exists(filePath))
//                     {
//                         // Construct the file URL and add to the list
//                         var fileUrl = Path.Combine(_host, $"{product.OwnerId}/{name}")
//                             .Replace("\\", "/");
//                         imageUrls.Add(fileUrl);
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error: {ex.Message}");
//                 // Optionally log the exception
//                 return new List<string>(); // Return an empty list on error
//             }

//             return imageUrls;
//         }
//     }
// }
