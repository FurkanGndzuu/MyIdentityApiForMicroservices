using Identity.Models.Dtos.Auth;
using Identity.Services.Auth;
using Identity.Services.Role;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly IRoleService _roleService;


        public AuthController(IAuthService authService, IRoleService roleService)
        {
            _authService = authService;
            _roleService = roleService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(SignUpModelDto model)
        {
            await _authService.SignupAsync(model);
            return Ok();
        }

        [HttpPost("connect/token")]
        public async Task<IActionResult> Login(LoginModelDto model) => Ok(await _authService.LoginAsync(model));



        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> GetRoles() { return Ok(await _roleService.GetRoles()); }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
           await _authService.SignOut();
            return Ok();
        }
    }
}
