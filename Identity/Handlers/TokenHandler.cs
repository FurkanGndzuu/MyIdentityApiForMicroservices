using Identity.Configurations;
using Identity.Models.Dtos.Token;
using Identity.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        public IConfiguration Configuration { get; set; }
        readonly UserManager<AppUser> _userManager;
        public TokenHandler(UserManager<AppUser> userManager,  IConfiguration configuration)
        {

            _userManager = userManager;
            Configuration = configuration;
        }

        public async Task<TokenDto> CreateAccessToken(AppUser user)
        {
            TokenDto token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]!));

            SigningCredentials signinCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);


            token.Expiration = DateTime.UtcNow.AddMonths(1);

            IList<Claim> authClaims = new List<Claim>();

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            authClaims.Add(new Claim(ClaimTypes.Email, user.Email));


            JwtSecurityToken securityToken
                 = new(issuer: Configuration["JWT:Issuer"],
                 audience: Configuration["JWT:Audience"],
                 expires: token.Expiration,
                 notBefore: DateTime.UtcNow,
                 signingCredentials: signinCredentials,
                 claims: authClaims
                 );

            JwtSecurityTokenHandler securityTokenHandler = new();

            token.AccessToken = securityTokenHandler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();
            return token;

        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
