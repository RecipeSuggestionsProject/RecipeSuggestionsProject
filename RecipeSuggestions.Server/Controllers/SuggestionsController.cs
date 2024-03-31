/*
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
            //Παίρνουμε το id των υλικών που έδωσε ο χρήστης

            var IngredientId = await Task.WhenAll(ingredients.Select(ingredient => _ingredientsService.GetIngredientIdByNameAsync(ingredient.Name!)));

            if(IngredientId!= null)
            {
                //Το δίνουμε σε καποια μέθοδο(συνταγών;συσχετίσεων) που επιστρέφει όλες τις συνταγές που περιέχουν όλα τα υλικά του χρήστη
                //Να έχουμε και μία που επιστρέφει για τουλάχιστον ένα υλικό;

                //var recipes= await _recipesService.GetRecipesByAllIngredientId(IngredientId);

            }

            //IngredientId = IngredientId.Where(IngredientId => IngredientId.HasValue).Select(Id => Id.Value).ToArray();


            //Το δίνουμε σε καποια μέθοδο(συνταγών;συσχετίσεων) που επιστρέφει όλες τις συνταγές που περιέχουν όλα τα υλικά του χρήστη


            // mapping συνταγών;

            //return Ok(recipeDTO); 


        }
    }
}

*/