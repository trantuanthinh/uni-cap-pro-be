using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Middleware
{
    public class JWTService(IOptions<JwtSettings> jwtSettings)
    {
        private readonly string _secretKey = jwtSettings.Value.SecretKey;
        private readonly string _issuer = jwtSettings.Value.Issuer;
        private readonly string _audience = jwtSettings.Value.Audience;

        public string GenerateJwtToken(User user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256Signature
            );

            // Prepare claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // unique id for each token
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username), // unique user username
                new Claim(JwtRegisteredClaimNames.Email, user.Email), // user email
                new Claim(JwtRegisteredClaimNames.Sub, user.Type.ToString()), // user type (Seller or Buyer)
            };

            // Create the token descriptor
            //Expires = DateTime.UtcNow.AddHours(1), // Token validity
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _issuer,
                Audience = _audience,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };

            // Create and return the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
