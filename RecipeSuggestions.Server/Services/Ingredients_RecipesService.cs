using Microsoft.EntityFrameworkCore;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Services
{
    public class Ingredients_RecipesService : IIngredients_RecipesService
    {
        private readonly RecipeSuggestionsServerContext _context;

        public Ingredients_RecipesService(
            RecipeSuggestionsServerContext context
        )
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient_Recipe>> GetAllIngredients_RecipesAsync()
        {
            return await _context.Ingredient_Recipe.ToListAsync();
        }

        public async Task<Ingredient_Recipe?> GetIngredient_RecipeAsync((int recipeId, int ingredientId) id)
        {
            Ingredient_Recipe? ingredient_recipe;
            try
            {
                ingredient_recipe = await _context.Ingredient_Recipe.FindAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return ingredient_recipe;
        }

        public async Task<Ingredient_Recipe> AddIngredient_RecipeAsync(Ingredient_Recipe ingredient_recipe)
        {
            ingredient_recipe = _context.Ingredient_Recipe.Add(ingredient_recipe).Entity;
            await _context.SaveChangesAsync();

            return ingredient_recipe;
        }

        public async Task EditIngredient_RecipeAsync((int recipeId, int ingredientId) id, Ingredient_Recipe ingredient_recipe)
        {
            if (id.recipeId != ingredient_recipe.RecipeId || id.ingredientId == ingredient_recipe.IngredientId)
            {
                return;
            }

            _context.Entry(ingredient_recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Ingredient_RecipeExists(id))
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task EditIngredients_RecipesOfRecipeAsync(int recipeId, IEnumerable<Ingredient_Recipe> ingredients_recipes)
        {
            // Ignore ingredient recipe relations with wrong recipe id.
            ingredients_recipes = ingredients_recipes.Where(
                ingredient_recipe => ingredient_recipe.RecipeId != recipeId
            ).ToArray();


            var oldIngredients_recipes = _context.Ingredient_Recipe.Where(
                ingredient_recipe => ingredient_recipe.RecipeId == recipeId
            );


            // Delete ingredient recipe relations containing IngredientIds not in new relation set
            // (relations about Ingredients the Recipe will no longer contain)
            foreach (var ingredient_recipeToDelete in oldIngredients_recipes.Where(
                oldIngredient_recipe => !ingredients_recipes.Any(
                    ingredient_recipe => ingredient_recipe.IngredientId == oldIngredient_recipe.IngredientId
                )
            ))
            {
                await DeleteIngredient_RecipeAsync(
                    (ingredient_recipeToDelete.RecipeId, ingredient_recipeToDelete.IngredientId)
                );
            }


            foreach (var ingredient_recipe in ingredients_recipes)
            {
                // Check whether a relation already exists about this Ingredient
                var old_ingredient_recipe = oldIngredients_recipes.FirstOrDefault(
                    oldIngredient_recipe => oldIngredient_recipe.IngredientId == ingredient_recipe.IngredientId
                );

                if (old_ingredient_recipe != null) // Edit existing relation
                {
                    await EditIngredient_RecipeAsync(
                        (old_ingredient_recipe.RecipeId, old_ingredient_recipe.IngredientId), 
                        ingredient_recipe
                    );
                } else // Add relation about new Ingredient
                {
                    await AddIngredient_RecipeAsync(ingredient_recipe);
                }
            }
        }

        public async Task DeleteIngredient_RecipeAsync((int recipeId, int ingredientId) id)
        {
            Ingredient_Recipe? ingredient_recipe;

            try
            {
                ingredient_recipe = await _context.Ingredient_Recipe.FindAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }

            if (ingredient_recipe != null)
            {
                _context.Ingredient_Recipe.Remove(ingredient_recipe);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            var ingredients_recipesOfRecipe = _context.Ingredient_Recipe.Where(
                ingredient_recipe => ingredient_recipe.RecipeId == recipeId
            );

            IEnumerable<Ingredient> ingredients = new Ingredient[ingredients_recipesOfRecipe.Count()];
            foreach (Ingredient_Recipe ingredient_recipe in ingredients_recipesOfRecipe)
            {
                ingredients.Append(ingredient_recipe.Ingredient);
            }

            return ingredients;
        }


        private bool Ingredient_RecipeExists((int recipeId, int ingredientId) id)
        {
            return _context.Ingredient_Recipe.Any(e => e.RecipeId == id.recipeId && e.IngredientId == id.ingredientId);
        }
    }
}
    