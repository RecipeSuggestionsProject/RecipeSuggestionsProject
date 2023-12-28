using System;
//using AutoMapper
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using RecipeSuggestions.Server.Interfaces;
//using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly RecipeSuggestionsServerContext _context;
        //private readonly IMapper _mapper
        public IngredientsService(RecipeSuggestionsServerContext context)
        {
            _context = context;
            //_mapper=mapper;
        }
        //to ingredient tha ginei IngredientDTO
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            //var ingredients=await _context.Ingredient.ToListAsync();
            return await _context.Ingredient.ToListAsync();
            //return _mapper.Map<IEnumerable<IngredientDTO>>(ingredients);
        }

        public async Task<Ingredient> GetIngredientIDAsync(int id)
        {
            var ingredient =await _context.Ingredient.FindAsync(id);
            return ingredient!;
        }

        //IngredientDTO ingredient
        public async Task<int> AddIngredientAsync(Ingredient ingredient)
        {
            //var ingredient=_mapper.Map<Ingredient>(ingredientDTO);
            _context.Ingredient.Add(ingredient);
            await _context.SaveChangesAsync();
            return ingredient.Id;
        }

        //
        public async Task<bool> UpdateIngredientAsync(int id, Ingredient ingredient)
        {
            if(!IngredientExists(id))
            {
                return false;
            }
            _context.Entry(ingredient).State=EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
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
