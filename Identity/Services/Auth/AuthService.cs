using Identity.Handlers;
using Identity.Models.Dtos.Auth;
using Identity.Models.Dtos.Token;
using Identity.Models.Entities;
using Identity.Services.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services.Auth
{
    public class AuthService : IAuthService
    {
        
        readonly UserManager<AppUser> _userManager;
        readonly IRoleService _roleService;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;


        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IRoleService roleService)
        {
          
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _roleService = roleService;
        }

        public async Task<TokenDto> LoginAsync(LoginModelDto loginModel)
        {
            if (loginModel == null)
            {
            
                await _signInManager.SignOutAsync();
                throw new Exception();
            }

            AppUser? user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                throw new Exception();
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, true, false);

            if (result.Succeeded && user is not null)
            {
                TokenDto token = await _tokenHandler.CreateAccessToken(user: user);

                return token;
            }

            throw new Exception();


        }

        public async Task SignOut()
        {
          await  _signInManager.SignOutAsync();
        }

        public async Task SignupAsync(SignUpModelDto signupModel)
        {
            if (signupModel is null)
            {
               
                throw new Exception();
            }

            AppUser user = new()
            {
                UserName = signupModel.Username,
                Email = signupModel.Email,
                PhoneNumber = signupModel.Phonenumber
            };


            IdentityResult result = await _userManager.CreateAsync(user, signupModel.Password);

            await _roleService.RoleAssign(user, "User");

                         
        }
    }
}
