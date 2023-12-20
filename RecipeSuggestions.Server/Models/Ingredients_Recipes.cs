namespace RecipeSuggestions.Server.Models
{
    public class Ingredients_Recipes
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
    }
}