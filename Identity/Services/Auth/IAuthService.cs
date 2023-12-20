using Identity.Models.Dtos.Auth;
using Identity.Models.Dtos.Token;

namespace Identity.Services.Auth
{
    public interface IAuthService
    {
        Task SignupAsync(SignUpModelDto signupModel);
        Task<TokenDto> LoginAsync(LoginModelDto loginModel);
        Task SignOut();
    }
}
