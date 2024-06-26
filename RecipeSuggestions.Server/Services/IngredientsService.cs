﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly RecipeSuggestionsServerContext _context;
        public IngredientsService(RecipeSuggestionsServerContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            
            return await _context.Ingredient.ToListAsync();
            
        }
        public async Task<Ingredient> GetIngredientIDAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID", nameof(id));
            }
            
                return await _context.Ingredient.FindAsync(id);
            
        }

        public async Task<int?> GetIngredientIdByNameAsync(string IngredientName)
        {
            if(string.IsNullOrEmpty(IngredientName))
            {
                return null;
            }
            var ingredient = await _context.Ingredient.FirstOrDefaultAsync(i => i.Name!= null && i.Name.Equals(IngredientName));
            if(ingredient!= null)
            {
                return ingredient?.Id;
            }
            else
            {
                return null;
            }
        }

        
        public async Task<int?> AddIngredientAsync(Ingredient ingredient)
        {
            var oldIngredient= await GetIngredientIdByNameAsync(ingredient!.Name!);

            if (oldIngredient.HasValue)
           {
                return oldIngredient.Value;
            }
            else
           {
                _context.Ingredient.Add(ingredient);
                await _context.SaveChangesAsync();
                return ingredient.Id;
            }
        }

        
        public async Task<bool> UpdateIngredientAsync(int id, Ingredient updatedIngredient)
        {
            if(!IngredientExists(id))
            {
                return false;
            }
            _context.Entry(updatedIngredient).State=EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return true;
        }

    
        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var ingredient = await _context.Ingredient.FindAsync(id);
            if(ingredient == null)
            {
                return false;
            }
            _context.Ingredient.Remove(ingredient);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredient.Any(e => e.Id == id);
        }
    }
}
