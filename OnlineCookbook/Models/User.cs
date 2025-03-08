using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineCookbook.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; } = string.Empty;

        // Relacja z Recipe 
        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
