using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly RecipeSuggestionsServerContext _context;

        public RecipesService(RecipeSuggestionsServerContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipe.ToListAsync();
        }

        public async Task<Recipe?> GetRecipeAsync(int id)
        {
            Recipe? recipe;
            try
            {
                recipe = await _context.Recipe.FindAsync(id);
                
            }
            catch (InvalidOperationException) {
                throw;
            }

            return recipe;
        }

        public async Task<Recipe> AddRecipeAsync(Recipe recipe)
        {
            _context.Recipe.Add(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task EditRecipeAsync(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return;
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteRecipeAsync(int id)
        {
            Recipe? recipe;
            
            try
            {
                recipe = await _context.Recipe.FindAsync(id);
            }
            catch (InvalidOperationException) {
                throw;
            }

            if (recipe != null)
            {
                _context.Recipe.Remove(recipe);
                await _context.SaveChangesAsync();
            }

        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}
