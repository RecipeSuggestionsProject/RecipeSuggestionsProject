using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeSuggestions.Server.Models
{
    [PrimaryKey(nameof(RecipeId), nameof(IngredientId))]
    public class Ingredient_Recipe
    {
        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        
        [ForeignKey(nameof(Ingredient))]
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        public int Quantity { get; set; }
        public string? QuantityType { get; set; }
    }
}