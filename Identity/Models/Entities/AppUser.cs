using Microsoft.AspNetCore.Identity;

namespace Identity.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
