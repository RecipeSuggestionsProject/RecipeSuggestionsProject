using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Interfaces
{
    public interface IIngredients_RecipesService
    {
        Task<IEnumerable<Ingredient_Recipe>> GetAllIngredients_RecipesAsync();
        Task<Ingredient_Recipe?> GetIngredient_RecipeAsync((int recipeId, int ingredientId) id);
        Task<Ingredient_Recipe> AddIngredient_RecipeAsync(Ingredient_Recipe ingredient_recipe);
        Task EditIngredient_RecipeAsync((int recipeId, int ingredientId) id, Ingredient_Recipe ingredient_recipe);
        Task EditIngredients_RecipesOfRecipeAsync(int recipeId, IEnumerable<Ingredient_Recipe> ingredients_recipes);
        Task DeleteIngredient_RecipeAsync((int recipeId, int ingredientId) id);
        IEnumerable<Ingredient> GetIngredientsByRecipeId(int recipeId);
    }
}
