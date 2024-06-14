using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RecipeSuggestions.Server.Controllers;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Mappings;
using RecipeSuggestions.Server.Services;
using System.Text.Json;

namespace RecipeSuggestions.Server.Integration_Tests
{
    [TestFixture]
    public class RecipesControllerIntegrationTests
    {
        private readonly RecipesController _recipesController;
        private readonly RecipeSuggestionsServerContext _recipeContext;
        private RecipeDTO? _createdRecipe;

        public RecipesControllerIntegrationTests()
        {
            var fileName = "../../../appsettings.json";
            var json = File.ReadAllText(fileName);
            var connectionString = 
                JsonSerializer.Deserialize<AppSettings>(json)?
                .ConnectionStrings?.RecipeSuggestionsServerContext
            ;

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<RecipeSuggestionsServerContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString);
            _recipeContext = new RecipeSuggestionsServerContext(dbContextOptionsBuilder.Options);

            var recipesService = new RecipesService(_recipeContext);
            var ingredients_RecipesService = new Ingredients_RecipesService(_recipeContext);
            var ingredientsService = new IngredientsService(_recipeContext);
            var ingredientMapper = new IngredientMapper();
            var relationMapper = new RelationMapper(ingredientsService, ingredientMapper);
            var recipeMapper = new RecipeMapper(ingredients_RecipesService, ingredientMapper, relationMapper);

            _recipesController = new RecipesController(
                 recipesService,
                 ingredients_RecipesService,
                 ingredientsService,
                 recipeMapper
             );
        }

        [Test, Order(1)]
        public async Task PostRecipeTest()
        {
            var recipeDTO = new RecipeDTO
            {
                Name = "Pancakes",
                Description =
                    "Put the flour, eggs, milk, 1 tbsp oil and a pinch of salt into a bowl " +
                    " or large jug, then whisk to a smooth batter.\n" + 
                    "Set aside for 30 mins to rest if you have time, or start cooking straight away.\n" + 
                    "Set a medium frying pan or crepe pan over a medium heat and carefully wipe it with" + 
                    " some oiled kitchen paper.\nWhen hot, cook your pancakes for 1 min on each side until" + 
                    " golden, keeping them warm in a low oven as you go.\nServe with lemon wedges and sugar, " + 
                    "or your favourite filling.\nOnce cold, you can layer the pancakes between baking parchment, " + 
                    "then wrap in cling film and freeze for up to 2 months.",
                Portions = 2,
                DurationInMinutes = 35.0f,
                Ingredients =
                [
                    new IngredientWithQuantity
                    {
                        Ingredient = new IngredientDTO
                        {
                            Name = "Flour"
                        },
                        Quantity = 100,
                        QuantityType = "grams"
                    },
                    new IngredientWithQuantity
                    {
                        Ingredient = new IngredientDTO
                        {
                            Name = "Milk"
                        },
                        Quantity = 300,
                        QuantityType = "grams"
                    }
                ]
            };

            var createdRecipeActionResult = (await _recipesController.PostRecipe(recipeDTO));

            Assert.NotNull(createdRecipeActionResult);
            Assert.IsInstanceOf<ActionResult<RecipeDTO>>(createdRecipeActionResult);

            _createdRecipe = (RecipeDTO)((CreatedAtActionResult)createdRecipeActionResult!.Result).Value;

            Assert.NotNull(_createdRecipe);

            

        }

        [Test, Order(2)]
        public async Task RecipeExistsAfterPostRecipe()
        {
            Assert.NotNull(_createdRecipe);
            Assert.NotNull(await _recipeContext.Recipe.FindAsync(_createdRecipe!.Id));
        }

        [Test, Order(2)]
        public async Task IngredientsExistAfterPostRecipe()
        {
            Assert.NotNull(_createdRecipe);
            foreach (IngredientWithQuantity ingredientWithQuantity in _createdRecipe!.Ingredients)
            {
                Assert.NotNull(ingredientWithQuantity);
                Assert.NotNull(ingredientWithQuantity.Ingredient);
                Assert.NotNull(
                    await _recipeContext.Ingredient.FindAsync(ingredientWithQuantity!.Ingredient?.Id)
                );
            }
        }

        [Test, Order(2)]
        public async Task Ingredients_RecipesExistAfterPostRecipe()
        {
            Assert.NotNull(_createdRecipe);
            foreach (IngredientWithQuantity ingredientWithQuantity in _createdRecipe!.Ingredients)
            {
                Assert.NotNull(ingredientWithQuantity);
                Assert.NotNull(ingredientWithQuantity.Ingredient);
                Assert.NotNull(
                    await _recipeContext.Ingredient_Recipe.FindAsync((_createdRecipe!.Id, ingredientWithQuantity!.Ingredient?.Id))
                );
            }
        }

        [Test, Order(3)]
        public async Task DeleteRecipeTest()
        {
            Assert.NotNull(_createdRecipe);
            await _recipesController.DeleteRecipe(_createdRecipe!.Id);
            Assert.Null(await _recipeContext.Recipe.FindAsync(_createdRecipe!.Id));
        }
    }
}
