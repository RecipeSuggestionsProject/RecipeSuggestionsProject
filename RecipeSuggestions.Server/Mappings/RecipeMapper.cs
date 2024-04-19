using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;
using System.Diagnostics;

namespace RecipeSuggestions.Server.Mappings
{
    public class RecipeMapper : IRecipeMapper
    {
        private readonly IIngredients_RecipesService _ingredients_recipesService;
        private readonly IIngredientMapper _ingredientMapper;
        private readonly IRelationMapper _relationMapper;
        public RecipeMapper(
            IIngredients_RecipesService ingredients_recipesService, 
            IIngredientMapper ingredientMapper, 
            IRelationMapper relationMapper
        )
        {
            _ingredients_recipesService = ingredients_recipesService;
            _ingredientMapper = ingredientMapper;
            _relationMapper = relationMapper;
        }
        public RecipeDTO Map(Recipe recipe)
        {
            return new RecipeDTO
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Portions = recipe.Portions,
                DurationInMinutes = recipe.DurationInMinutes,
                Ingredients = _relationMapper.Map(
                   _ingredients_recipesService.GetIngredients_RecipesOfRecipeAsync(recipe.Id)
                )
            };
        }

        public RecipeWithIngredients Map(RecipeDTO recipeDTO)
        {
            var recipe = new Recipe
            {
                Id = recipeDTO.Id,
                Name = recipeDTO.Name,
                Description = recipeDTO.Description,
                Portions = recipeDTO.Portions,
                DurationInMinutes = recipeDTO.DurationInMinutes
            };

            var ingredients_recipes = new List<Ingredient_Recipe>();
            foreach (var ingredientWithQuantity in recipeDTO.Ingredients)
            {
                if (ingredientWithQuantity.Ingredient != null)
                {
                    ingredients_recipes.Add(
                        new Ingredient_Recipe
                        {
                            RecipeId = recipeDTO.Id,
                            Recipe = recipe,
                            IngredientId = ingredientWithQuantity.Ingredient.Id,
                            Ingredient = _ingredientMapper.Map(ingredientWithQuantity.Ingredient),
                            Quantity = ingredientWithQuantity.Quantity,
                            QuantityType = ingredientWithQuantity.QuantityType
                        }
                    );
                }
            }

            return new RecipeWithIngredients
            {
                Recipe = recipe,
                Ingredients_Recipes = ingredients_recipes
            };
        }
    }
}
