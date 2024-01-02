using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSuggestions.Server.Data.Interfaces;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Services
{
    public class RecipesService : IRecipesService
    {
        public IRecipeSuggestionsServerContext Context { get; }

        public RecipesService(IRecipeSuggestionsServerContext context) {
            Context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await Context.Recipe.ToListAsync();
        }

        public async Task<Recipe?> GetRecipeAsync(int id)
        {
            Recipe? recipe;
            try
            {
                recipe = await Context.Recipe.FindAsync(id);
                
            }
            catch (InvalidOperationException) {
                throw;
            }

            return recipe;
        }

        public async Task<Recipe> AddRecipeAsync(Recipe recipe)
        {
            Context.Recipe.Add(recipe);
            await Context.SaveChangesAsync();

            return recipe;
        }

        public async Task EditRecipeAsync(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return;
            }

            Context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
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
                recipe = await Context.Recipe.FindAsync(id);
            }
            catch (InvalidOperationException) {
                throw;
            }

            if (recipe != null)
            {
                Context.Recipe.Remove(recipe);
                await Context.SaveChangesAsync();
            }

        }

        private bool RecipeExists(int id)
        {
            return Context.Recipe.Any(e => e.Id == id);
        }
    }
}
