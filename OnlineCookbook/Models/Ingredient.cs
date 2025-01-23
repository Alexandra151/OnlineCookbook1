using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineCookbook.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Relacja wiele-do-wielu z Recipes
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
