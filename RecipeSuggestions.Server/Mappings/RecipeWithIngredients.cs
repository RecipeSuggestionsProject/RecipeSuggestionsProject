using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Mappings
{
    public class RecipeWithIngredients
    {
        public Recipe? Recipe { get; set; }
        public IEnumerable<Ingredient_Recipe>? Ingredients_Recipes { get; set; }
    }
}
