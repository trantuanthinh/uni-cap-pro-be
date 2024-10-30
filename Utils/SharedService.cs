using System.Drawing;
using System.Text.RegularExpressions;

namespace uni_cap_pro_be.Utils
{
    // DONE
    public class SharedService
    {
        public string ImageToBase64(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Convert IFormFile to a byte array
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();

                // Convert byte array to Base64 string
                return "data:image/png;base64," + Convert.ToBase64String(bytes);
            }
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string notHashedPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(notHashedPassword, hashedPassword);
        }

        public bool IsValidGmail(string email)
        {
            Regex GmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", RegexOptions.Compiled);
            if (string.IsNullOrEmpty(email))
                return false;

            return GmailRegex.IsMatch(email);
        }

        public bool IsNumber(string input)
        {
            return double.TryParse(input, out _);
        }
    }
}
