using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NUnit.Framework;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Models;
using Microsoft.Extensions.DependencyInjection;
using RecipeSuggestions.Server.Services;
using System.Linq.Expressions;

namespace RecipeSuggestions.Server.Services.UnitTests
{
    [TestFixture]
    public class AddIngredientUnitTest
    {
        private Mock<RecipeSuggestionsServerContext> _mockcontext;
        private Mock<DbSet<Ingredient>> _mockIngredientSet;
        private IngredientsService _ingredientsService;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockcontext=new Mock<RecipeSuggestionsServerContext>();
            _mockIngredientSet=new Mock<DbSet<Ingredient>>();

            var data = new List<Ingredient>
            {
            }.AsQueryable();

            var mockAsyncProvider = new Mock<IAsyncQueryProvider>();

            _mockIngredientSet.As<IQueryable<Ingredient>>().Setup(mockSet => mockSet.Provider).Returns(mockAsyncProvider.Object);
            _mockIngredientSet.As<IQueryable<Ingredient>>().Setup(mockSet => mockSet.Expression).Returns(data.Expression);
            _mockIngredientSet.As<IQueryable<Ingredient>>().Setup(mockSet => mockSet.ElementType).Returns(data.ElementType);
            _mockIngredientSet.As<IQueryable<Ingredient>>().Setup(mockSet => mockSet.GetEnumerator()).Returns(data.GetEnumerator());

            _mockcontext.Setup(mockContext => mockContext.Ingredient).Returns(_mockIngredientSet.Object);
            _ingredientsService = new IngredientsService(_mockcontext.Object);
        }

        [Test]
        public async Task AddIngredientTest()
        {
            //Arrange
            var ingredient = new Ingredient
            {
                Name = "TestIngredient",
                Type = "TestType",
                Description = "This is a test description"
            };

            // Act
            var test_result = await _ingredientsService.AddIngredientAsync(ingredient);

            // Assert
            _mockIngredientSet.Verify(set => set.Add(It.IsAny<Ingredient>()), Times.Once());
            _mockcontext.Verify(a => a.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

           
            Assert.IsNotNull(test_result); 
            Assert.AreEqual(ingredient.Id, test_result);      
        }
    }
}




