using AutoMapper;
using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Mappings
{
    public class SetRecipeDTOIngredientsAction : IMappingAction<Recipe, RecipeDTO> {
        private readonly IMapper _mapper;
        private readonly IIngredients_RecipesService _ingredients_recipesService;
        public SetRecipeDTOIngredientsAction(IMapper mapper, IIngredients_RecipesService ingredients_recipesService) {
            _mapper = mapper;
            _ingredients_recipesService = ingredients_recipesService;
        }

        public void Process(Recipe recipe, RecipeDTO recipeDTO, ResolutionContext context) {
            recipeDTO.Ingredients = 
                _mapper.Map<IEnumerable<IngredientWithQuantity>>(
                    _ingredients_recipesService.GetIngredients_RecipesOfRecipeAsync(recipe.Id)
            );
        }
    }
}
