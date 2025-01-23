using Microsoft.AspNetCore.Identity;

namespace OnlineCookbook.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; } = string.Empty; // Dodano właściwość Role
    }
}
