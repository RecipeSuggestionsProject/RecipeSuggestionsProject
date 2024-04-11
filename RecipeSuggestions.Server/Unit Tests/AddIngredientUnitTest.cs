using Moq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Models;
using Microsoft.Extensions.DependencyInjection;
using RecipeSuggestions.Server.Services;


namespace RecipeSuggestions.Server.Services.UnitTests
{
    [TestFixture]
    public class AddIngredientUnitTest
    {
        private Mock<RecipeSuggestionsServerContext> _mockcontext;
        private Mock<DbSet<Ingredient>> _mockIngredientSet;
        private IngredientsService _mockingredientService;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockcontext=new Mock<RecipeSuggestionsServerContext>();
            _mockIngredientSet=new Mock<DbSet<Ingredient>>();

            _mockcontext.Setup(i => i.Set<Ingredient>()).Returns(_mockIngredientSet.Object);


            _mockingredientService=new IngredientsService(_mockcontext.Object);

        }

        [Test]
        public async Task AddIngredientTest()
        {
            // Arrange
            var ingredient = new Ingredient
            {
                Name = "TestIngredient",
                Type = "TestType",
                Description = "This is a test description"
            };

            // Act
            var test_result = await _mockingredientService.AddIngredientAsync(ingredient);

            // Assert
            _mockIngredientSet.Verify(set => set.Add(It.IsAny<Ingredient>()), Times.Once());
            _mockcontext.Verify(a => a.SaveChanges(), Times.Once());

           
            Assert.IsNotNull(test_result); 
            Assert.AreEqual(ingredient.Id, test_result); 
            
        }



    }
}




