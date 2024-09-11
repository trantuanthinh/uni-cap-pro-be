using System.Text.RegularExpressions;

namespace uni_cap_pro_be.Utils
{
    // DONE
    public class SharedService
    {
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
