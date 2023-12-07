using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using IntelligentStore.Domain.IInfrastructureServiceInterface;
using Shared.Models;
using Shared.Storage;
using Shared;

namespace IntelligentStore.Infrastructure.Token
{
    public class TokenService : ITokenService
    {
        public string APPLICATION_JWT_KEY { get; } = CookieKeys.APPLICATION_JWT_KEY;

        private readonly SymmetricSecurityKey _key;
        private readonly IRequestStorage _requestStorage;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenService(IRequestStorage requestStorage)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ETB4GNHYY2ETB4GNHYY2"));
            _requestStorage = requestStorage;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<string> CreateToken(string id, string email)
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Email, email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = _tokenHandler.WriteToken(token);

            _requestStorage.SetCookie(APPLICATION_JWT_KEY, jwtToken);

            Console.WriteLine("token invoked" + jwtToken);

            return jwtToken;
        }

        public ClaimUser GetTokenData(string token)
        {
            JwtSecurityToken jwtToken = _tokenHandler.ReadJwtToken(token);

            Console.WriteLine(jwtToken);

            var userId = jwtToken.Claims?.FirstOrDefault(c => c.Type == "nameid").Value;

            var email = jwtToken.Claims?.FirstOrDefault(c => c.Type == "email").Value;

            return new ClaimUser { Id = userId, Email = email };
        }
    }
}
