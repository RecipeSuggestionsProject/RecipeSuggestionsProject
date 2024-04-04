using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;
using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Mappings;

namespace RecipeSuggestions.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesService _recipesService;
        private readonly IIngredients_RecipesService _ingredients_RecipesService;
        private readonly IIngredientsService _ingredientsService;
        private readonly IMapper _mapper;

        public RecipesController(
            IRecipesService recipesService, 
            IIngredients_RecipesService ingredients_RecipesService,
            IIngredientsService ingredientsService,
        IMapper mapper
        ) {
            _recipesService = recipesService;
            _ingredients_RecipesService = ingredients_RecipesService;
            _ingredientsService = ingredientsService;
            _mapper = mapper;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe()
        {
            return Ok(await _recipesService.GetAllRecipesAsync());
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            Recipe? recipe;
            try
            {
                recipe = await _recipesService.GetRecipeAsync(id);
            } catch (InvalidOperationException)
            {
                return NotFound();
            }

            return recipe != null ? recipe : NotFound();
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            try
            {
                await _recipesService.EditRecipeAsync(id, recipe);
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

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> PostRecipe(RecipeDTO recipe)
        {
            var recipeWithIngredients = _mapper.Map<RecipeWithIngredients>(recipe);
            if (recipeWithIngredients.Recipe == null) { return BadRequest(); }

            // Add Recipe
            recipe = _mapper.Map<RecipeDTO>(await _recipesService.AddRecipeAsync(recipeWithIngredients.Recipe));
            foreach (var ingredient_recipe in recipeWithIngredients.Ingredients_Recipes) {
                ingredient_recipe.RecipeId = recipe.Id;
            }
            

            if (recipeWithIngredients.Ingredients_Recipes != null)
            {
                foreach (var ingredient_recipe in recipeWithIngredients.Ingredients_Recipes)
                {
                    if (ingredient_recipe.Ingredient?.Name != null)
                    {
                        int? ingredientId = await _ingredientsService.GetIngredientIdByNameAsync(ingredient_recipe.Ingredient.Name!);

                        // The ingredient does not exist in the database
                        if (ingredientId == null)
                        {
                            // Add the ingredient
                            ingredientId = await _ingredientsService.AddIngredientAsync(ingredient_recipe.Ingredient);
                        }

                        ingredient_recipe.IngredientId = (int)ingredientId;
                    }

                }

                // Overwrite the related Ingredients of this recipe
                await _ingredients_RecipesService.EditIngredients_RecipesOfRecipeAsync(
                    recipeWithIngredients.Recipe.Id,
                    //recipeWithIngredients.Ingredient.Id,
                    recipeWithIngredients.Ingredients_Recipes
                );
            }

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            try
            {
                await _recipesService.DeleteRecipeAsync(id);
            }
            catch (InvalidOperationException) { 
                return NotFound();
            }

            return NoContent();
        }
    }
}
