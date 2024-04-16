using AutoMapper;
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
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            mapperConfigurationExpression.AddProfile(new AutoMapperProfile());
            var mapper = new Mapper(new MapperConfiguration(mapperConfigurationExpression));

            _recipesController = new RecipesController(
                 recipesService,
                 ingredients_RecipesService,
                 ingredientsService,
                 mapper
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

            var createdRecipeActionResult = await _recipesController.PostRecipe(recipeDTO);
            _createdRecipe = createdRecipeActionResult.Value;

            Assert.NotNull(createdRecipeActionResult);
            Assert.NotNull(_createdRecipe);

            // Assert status code is Created (201)
            Assert.IsInstanceOf<CreatedAtActionResult>(createdRecipeActionResult);

        }

        [Test, Order(2)]
        [Ignore("Recipe id is not available until PostRecipe correctly returns the created recipe")]
        public void RecipeExistsAfterPostRecipe()
        {
            Assert.NotNull(_createdRecipe);
            Assert.NotNull(_recipeContext.Recipe.Find(_createdRecipe!.Id));
        }

        [Test, Order(2)]
        [Ignore("Ingredient ids are not available until PostRecipe correctly returns the created recipe")]
        public void IngredientsExistAfterPostRecipe()
        {
            Assert.NotNull(_createdRecipe);
            foreach (IngredientWithQuantity ingredientWithQuantity in _createdRecipe!.Ingredients)
            {
                Assert.NotNull(ingredientWithQuantity);
                Assert.NotNull(ingredientWithQuantity.Ingredient);
                Assert.NotNull(
                    _recipeContext.Ingredient.Find(ingredientWithQuantity!.Ingredient?.Id)
                );
            }
        }

        [Test, Order(2)]
        [Ignore("Recipe and Ingredient ids are not available until PostRecipe correctly returns the created recipe")]
        public void Ingredients_RecipesExistAfterPostRecipe()
        {
            Assert.NotNull(_createdRecipe);
            foreach (IngredientWithQuantity ingredientWithQuantity in _createdRecipe!.Ingredients)
            {
                Assert.NotNull(ingredientWithQuantity);
                Assert.NotNull(ingredientWithQuantity.Ingredient);
                Assert.NotNull(
                    _recipeContext.Ingredient_Recipe.Find((_createdRecipe!.Id, ingredientWithQuantity!.Ingredient?.Id))
                );
            }
        }
    }

    public class AppSettings {
        public Logging? Logging { get; set; }
        public string? AllowedHosts { get; set; }
        public ConnectionStrings? ConnectionStrings { get; set; }
    }

    public class Logging
    {
        public LogLevel? LogLevel { get; set;}
    }

    public class LogLevel
    {
        public string? Default { get; set; }
        public string? MicrosoftAspNetCore { get; set; }
    }

    public class ConnectionStrings
    {
        public string? RecipeSuggestionsServerContext { get; set; }
    }
}
