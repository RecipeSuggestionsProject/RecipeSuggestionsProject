using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Interfaces
{
    public interface IIngredientsService
    {
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<Ingredient> GetIngredientIDAsync(int id);
        Task<int> AddIngredientAsync(IngredientDTO ingredientDTO);
        Task<bool> UpdateIngredientAsync(int id, IngredientDTO updatedIngredientDTO);
        Task<bool> DeleteIngredientAsync(int id);
    }
}
