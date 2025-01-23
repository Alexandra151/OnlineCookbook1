namespace OnlineCookbook.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = new Recipe();

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = new Ingredient();
    }
}
