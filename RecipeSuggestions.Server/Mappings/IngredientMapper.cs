using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Mappings
{
    public class IngredientMapper : IIngredientMapper
    {
        public IngredientDTO Map(Ingredient ingredient)
        {
            return new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Type = ingredient.Type,
                Description = ingredient.Description
            };
        }

        public Ingredient Map(IngredientDTO ingredientDTO)
        {
            return new Ingredient
            {
                Id = ingredientDTO.Id,
                Name = ingredientDTO.Name,
                Type = ingredientDTO.Type,
                Description = ingredientDTO.Description
            };
        }
    }
}
