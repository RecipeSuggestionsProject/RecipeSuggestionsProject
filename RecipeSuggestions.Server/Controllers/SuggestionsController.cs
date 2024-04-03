using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using RecipeSuggestions.Server.Services;

namespace RecipeSuggestions.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : ControllerBase
    {
        private readonly IRecipesService _recipesService;
        private readonly IIngredients_RecipesService _ingredients_RecipesService;
        private readonly IIngredientsService _ingredientsService;
        private readonly IMapper _mapper;

        public SuggestionsController(IRecipesService recipesService, IIngredients_RecipesService ingredients_RecipesService,
         IIngredientsService ingredientsService, IMapper mapper)
        {
            _recipesService = recipesService;
            _ingredients_RecipesService = ingredients_RecipesService;
            _ingredientsService = ingredientsService;
            _mapper = mapper;
        }


        //Θέλουμε να επιστρέφει όλες τις συνταγές που δεν περιέχουν υλικά που δεν έχει ο χρήστης
        //ή αλλιως ολες τις συνταγες που περιέχουν μονο υλικα που εχει ο χρηστης
        [HttpPost]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> CreateSuggestions(IEnumerable<IngredientDTO> ingredients)
        {

            try
            {
                var IngredientRecipes = await _ingredients_RecipesService.GetAllIngredients_RecipesAsync();
                var IngredientId = await Task.WhenAll(ingredients.Select(ingredient => _ingredientsService.GetIngredientIdByNameAsync(ingredient.Name!)));

                if (IngredientId != null && IngredientId.Any()) //Διασφαλίζουμε οτι υπάρχει id για το υλικό και ότι δεν είναι null
                {
                    var FilteredRecipes = IngredientRecipes.Where(ingr_id => IngredientId.Contains(ingr_id.IngredientId)).ToList(); //Φιλτράρισμα με βάση τα διαθέσιμα υλικά του χρήστη
                    var RecipeIDs = FilteredRecipes.Select(ingr_id => ingr_id.RecipeId).Distinct(); //Παιρνουμε τα id των φιλτραρισμένων συνταγών 

                    var suggestions = new List<RecipeDTO>();

                    foreach (var recipeId in RecipeIDs)
                    {
                        var recipe = await _recipesService.GetRecipeAsync(recipeId);
                        if (recipe!= null)
                        {
                            suggestions.Add(_mapper.Map<RecipeDTO>(recipe));
                        }

                    }
                    if (suggestions.Any())
                    {
                        return Ok(suggestions);
                    }
                    else
                    {
                        return NotFound("Δεν βρέθηκαν συνταγές με τα επιλεγμένα υλικά");
                    }
                    
                }
                else
                {
                    return BadRequest("Δεν δόθηκαν υλικά");
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}


