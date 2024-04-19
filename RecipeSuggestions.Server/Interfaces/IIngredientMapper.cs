using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Interfaces
{
    public interface IIngredientMapper : IReMapper<Ingredient, IngredientDTO>, IReMapper<IngredientDTO, Ingredient> {}
}
