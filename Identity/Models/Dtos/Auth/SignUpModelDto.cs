using System.ComponentModel.DataAnnotations;

namespace Identity.Models.Dtos.Auth
{
    public class SignUpModelDto
    {
        public string? Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? Phonenumber { get; set; }
        public string Password { get; set; }
    }
}
