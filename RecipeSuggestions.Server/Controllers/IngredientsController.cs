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
using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Services;
using System.Collections;

namespace RecipeSuggestions.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;        
        private readonly IIngredientMapper _ingredientMapper;

        public IngredientsController(IIngredientsService ingredientsService, IIngredientMapper ingredientMapper)
        {
            _ingredientsService = ingredientsService;
            _ingredientMapper = ingredientMapper;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDTO>>> GetIngredient()
        {
            var ingredient = await _ingredientsService.GetIngredientsAsync();
            var ingrDTO= _ingredientMapper.Map(ingredient);
            return Ok(ingrDTO);
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDTO>> GetIngredient(int id)
        {
            var ingredient = await _ingredientsService.GetIngredientIDAsync(id);
            var ingredientDTO = _ingredientMapper.Map(ingredient);

            if (ingredientDTO == null)
            {
                return NotFound();
            }

            return Ok(ingredientDTO);
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, IngredientDTO ingredientDTO)
        {
            if (id != ingredientDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                var ingredient = await _ingredientsService.GetIngredientIDAsync(id);
                ingredient = _ingredientMapper.Map(ingredientDTO);

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

        
        [HttpPost]
        public async Task<ActionResult<IngredientDTO>> CreateIngredient(IngredientDTO ingredientDTO)
        {

            var ingredient = _ingredientMapper.Map(ingredientDTO);
            var newIngrId = await _ingredientsService.AddIngredientAsync(ingredient);

            if (!newIngrId.HasValue)
            {
                return BadRequest("Δεν δημιουργήθηκε κανένα υλικό"); 
            }

            int ingredientId = newIngrId.Value;

            var createdIngredient = await _ingredientsService.GetIngredientIDAsync(ingredientId);

            if (createdIngredient == null)
            {
                return NotFound(); 
            }

            var createdIngredientDTO = _ingredientMapper.Map(createdIngredient);

            return CreatedAtAction(nameof(GetIngredient), new { id = ingredientId }, createdIngredientDTO);
        }

        

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            try
            {
                var success = await _ingredientsService.DeleteIngredientAsync(id);
                if(!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

        }

        //private bool IngredientExists(int id)
       // {
       //     return _ingredientsService.Ingredient.Any(e => e.Id == id);
       // }
    }
}
