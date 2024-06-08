using Application.Config;
using Application.DTO;
using Application.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly CenterConfig _centerConfig;

        public JwtService(CenterConfig centerConfig)
        {
            _centerConfig = centerConfig;
        }

        public UserLoginResponse GenerateUserToken(UserDTO user)
        {
            var key = Encoding.ASCII.GetBytes(_centerConfig.JWTConfig.Secret);
            var expires = DateTime.UtcNow.AddDays(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,_centerConfig.JWTConfig.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("role", user.Role)
                }),

                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _centerConfig.JWTConfig.Issuer,
                Audience = _centerConfig.JWTConfig.Audience
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new UserLoginResponse
            {
                UserId = user.Id,
                Role = user.Role,
                Token = token,
                Expires = expires,
                User = user,
            };
        }
    }
}
