using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Interfaces
{
    public interface IRelationMapper : IReMapper<Ingredient_Recipe, IngredientWithQuantity> {}
}
