namespace RecipeSuggestions.Server.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Portions { get; set; }
        public float DurationInMinutes { get; set; }
        public IEnumerable<IngredientWithQuantity>? Ingredients { get; set; }
        
    }

    public class IngredientWithQuantity {
        public IngredientDTO? Ingredient { get; set; }
        public int Quantity { get; set; }
        public String? QuantityType { get; set; }
    }
}
