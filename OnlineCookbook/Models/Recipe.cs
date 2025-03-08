using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCookbook.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string? UserId { get; set; } 

        
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; } // Relacja do kategorii

        [ForeignKey("UserId")]
        public virtual User? User { get; set; } // Relacja do użytkownika

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
