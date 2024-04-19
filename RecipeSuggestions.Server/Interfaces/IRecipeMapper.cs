using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Mappings;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Interfaces
{
    public interface IRecipeMapper : IReMapper<Recipe, RecipeDTO>, IReMapper<RecipeDTO, RecipeWithIngredients> {}
}
