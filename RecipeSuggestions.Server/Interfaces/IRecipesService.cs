using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Interfaces
{
    public interface IRecipesService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeAsync(int id);
        Task<Recipe> AddRecipeAsync(Recipe recipe);
        Task EditRecipeAsync(int id, Recipe recipe);
        Task DeleteRecipeAsync(int id);
    }
}
