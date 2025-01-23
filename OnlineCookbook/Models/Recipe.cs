using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineCookbook.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(0, 5)]
        public decimal Rating { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();

        // Relacja wiele-do-wielu z Ingredients
        public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    }
}
