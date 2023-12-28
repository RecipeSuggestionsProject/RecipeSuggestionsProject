using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;
using RecipeSuggestions.Server.Services;

namespace RecipeSuggestions.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;
        // private readonly RecipeSuggestionsMapper _mapper;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            _ingredientsService=ingredientsService;
            //_mapper=mapper;
        }

        // GET: api/Ingredients
        //To Ingredient na ginei IngredientDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredient()
        {
            var ingredients = await _ingredientsService.GetIngredientsAsync();
            return Ok(ingredients);
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            var ingredient = await _ingredientsService.GetIngredientIDAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return BadRequest();
            }

            try
            {
                var success=await _ingredientsService.UpdateIngredientAsync(id,ingredient);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //IngredientDTO ingredientDT0
        public async Task<ActionResult<int>> CreateIngredient(Ingredient ingredient)
        {
            //ingredientDTO
            var newingrid = await _ingredientsService.AddIngredientAsync(ingredient);

            return CreatedAtAction("GetIngredient", new { id = newingrid }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            try
            {
                await _ingredientsService.DeleteIngredientAsync(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return NoContent();
        }

        //private bool IngredientExists(int id)
       // {
       //     return _ingredientsService.Ingredient.Any(e => e.Id == id);
       // }
    }
}
