using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Mappings
{
    public class RelationMapper : IRelationMapper
    {
        private readonly IIngredientsService _ingredientsService;
        private readonly IIngredientMapper _ingredientMapper;
        public RelationMapper(IIngredientsService ingredientsService, IIngredientMapper ingredientMapper)
        {
            _ingredientsService = ingredientsService;
            _ingredientMapper = ingredientMapper;
        }

        public IngredientWithQuantity Map(Ingredient_Recipe ingredient_recipe)
        {
            return new IngredientWithQuantity
            {
                Ingredient = (ingredient_recipe.Ingredient != null) ? _ingredientMapper.Map(
                        ingredient_recipe.Ingredient
                ) : null,
                Quantity = ingredient_recipe.Quantity,
                QuantityType = ingredient_recipe.QuantityType
            };
        }
    }
}
