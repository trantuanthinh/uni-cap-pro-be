using System.Text.RegularExpressions;

namespace uni_cap_pro_be.Utils
{
	public class SharedService
	{
		private readonly Regex GmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", RegexOptions.Compiled);

		public bool IsValidGmail(string email)
		{
			if (string.IsNullOrEmpty(email))
				return false;

			return GmailRegex.IsMatch(email);
		}
	}
}
