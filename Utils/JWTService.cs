using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Utils
{
	public class JWTService(IOptions<JwtSettings> jwtSettings)
	{
		private readonly string _secretKey = jwtSettings.Value.SecretKey;
		private readonly string _issuer = jwtSettings.Value.Issuer;
		private readonly string _audience = jwtSettings.Value.Audience;

		public string GenerateJwtToken(User user)
		{
			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
			var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			// Prepare claims
			var claims = new[]
			{
		new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		new Claim(JwtRegisteredClaimNames.Sub, user.Username),
		new Claim(JwtRegisteredClaimNames.Email, user.Email),
	};

			// Create the token descriptor
			//Expires = DateTime.UtcNow.AddHours(1), // Token validity
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Issuer = _issuer,
				Audience = _audience,
				SigningCredentials = credentials
			};

			// Create and return the token
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}
}
