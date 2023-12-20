using System.ComponentModel.DataAnnotations;

namespace Identity.Models.Dtos.Auth
{
    public class LoginModelDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
