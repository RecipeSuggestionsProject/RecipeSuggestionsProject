using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Interfaces
{
    public interface IIngredientsService
    {
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<Ingredient> GetIngredientIDAsync(int id);
        Task<int?> GetIngredientIdByNameAsync(string IngredientName);
        Task<int?> AddIngredientAsync(Ingredient ingredient);
        Task<bool> UpdateIngredientAsync(int id, Ingredient updatedIngredient);
        Task<bool> DeleteIngredientAsync(int id);
    }
}
