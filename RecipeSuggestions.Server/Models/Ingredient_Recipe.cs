namespace RecipeSuggestions.Server.Models
{
    public class Ingredient_Recipe
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public string? QuantityType { get; set; }
    }
}