using Identity.Models.Dtos.Token;
using Identity.Models.Entities;

namespace Identity.Handlers
{
    public interface ITokenHandler
    {
        Task<TokenDto> CreateAccessToken(AppUser user);
    }
}
