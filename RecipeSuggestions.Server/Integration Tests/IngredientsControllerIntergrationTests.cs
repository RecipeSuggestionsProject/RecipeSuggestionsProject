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
    public class IngredientsControllerIntegrationTests
    {
        private readonly IngredientsController _ingredientsController;
        private readonly RecipeSuggestionsServerContext _ingredientContext;
        private IngredientDTO? _createdIngredient;

        public IngredientsControllerIntegrationTests()
        {
            var fileName = "../../../appsettings.json";
            var json = File.ReadAllText(fileName);
            var connectionString = JsonSerializer.Deserialize<AppSettings>(json)?.ConnectionStrings?.RecipeSuggestionsServerContext;

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<RecipeSuggestionsServerContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString);

            _ingredientContext = new RecipeSuggestionsServerContext(dbContextOptionsBuilder.Options);

            var ingredientsService = new IngredientsService(_ingredientContext);
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            mapperConfigurationExpression.AddProfile(new AutoMapperProfile());
            var mapper = new Mapper(new MapperConfiguration(mapperConfigurationExpression));

            _ingredientsController = new IngredientsController(ingredientsService,mapper);
        }

        [Test,Order(1)]
        public async Task CreateIngredientTest()
        {
            var ingredientDTO = new IngredientDTO
            {
                Name = "Broccoli",
                Type = "Vegetable",
                Description = "Broccoli is an edible green plant in the cabbage family " +
                "whose large flowering head, stalk and small associated leaves are eaten as a vegetable "
            };

            var createdIngredientActionResult = await _ingredientsController.CreateIngredient(ingredientDTO);
            _createdIngredient = createdIngredientActionResult.Value;

            Assert.NotNull(createdIngredientActionResult);
            Assert.IsInstanceOf<ActionResult<IngredientDTO>>(createdIngredientActionResult);
        }

        
        [Test, Order(2)]
        public void IngredientExistsAfterCreateIngredient()
        {
            Assert.NotNull(_createdIngredient);
            Assert.NotNull(_ingredientContext.Ingredient.Find(_createdIngredient!.Id));
        }
        
    public class AppSettings
    {
        public Logging? Logging { get; set; }
        public string? AllowedHosts { get; set; }
        public ConnectionStrings? ConnectionStrings { get; set; }
    }

    public class Logging
    {
        public LogLevel? LogLevel { get; set; }
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
}
