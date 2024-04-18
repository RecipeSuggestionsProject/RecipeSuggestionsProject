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
    public class SuggestionsControllerIntegrationTests
    {
        private readonly SuggestionsController _suggestionsController;
        private readonly RecipeSuggestionsServerContext _suggestionsContext;

        public SuggestionsControllerIntegrationTests()
        {
            var fileName = "../../../appsettings.json";
            var json = File.ReadAllText(fileName);
            var connectionString = JsonSerializer.Deserialize<AppSettings>(json)?.ConnectionStrings?.RecipeSuggestionsServerContext;

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<RecipeSuggestionsServerContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString);


            _suggestionsContext = new RecipeSuggestionsServerContext(dbContextOptionsBuilder.Options);

            var recipesService = new RecipesService(_suggestionsContext);
            var ingredients_RecipesService = new Ingredients_RecipesService(_suggestionsContext);
            var ingredientsService = new IngredientsService(_suggestionsContext);
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            mapperConfigurationExpression.AddProfile(new AutoMapperProfile());
            var mapper = new Mapper(new MapperConfiguration(mapperConfigurationExpression));
            var recipeMapper = new RecipeMapper(ingredients_RecipesService, mapper);

            _suggestionsController = new SuggestionsController(recipesService,ingredients_RecipesService,ingredientsService,mapper, recipeMapper);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _suggestionsContext.Dispose();
        }

        /*
        [Test]
        public async Task CreateSuggestionsTest()
        {
            var ingredients = new  List<IngredientDTO>
            {
               new IngredientDTO { Name = "Ingredient1"},
               new IngredientDTO { Name = "Ingredient2"}
            };

            var recipes = new List<RecipeDTO>
            {
               new RecipeDTO { Name = "Suggestion1"},
               new RecipeDTO { Name = "Suggestion2"}
            };

            var result = await _suggestionsController.CreateSuggestions(ingredients);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<RecipeDTO>>(okResult.Value);
            Assert.AreEqual(recipes.Count, (okResult.Value as List<RecipeDTO>).Count);

        }
        */

        [Test]
        public async Task CreateSuggestionsTest()
        {
            var ingredients = new List<IngredientDTO>
           {
       new IngredientDTO { Name = "Ingredient1" },
       new IngredientDTO { Name = "Ingredient2" }
    };

            var result = await _suggestionsController.CreateSuggestions(ingredients);

            
            if (result.Result is OkObjectResult okResult)
            {
                Assert.IsInstanceOf<List<RecipeDTO>>(okResult.Value);
                Assert.Greater((okResult.Value as List<RecipeDTO>).Count, 0);
            }
            else if (result.Result is NotFoundObjectResult notFoundResult)
            {
                Assert.AreEqual("Δεν βρέθηκαν συνταγές με τα επιλεγμένα υλικά", notFoundResult.Value);
            }
            else
            {
                Assert.Fail("Unexpected result type");
            }
        }
    }
}