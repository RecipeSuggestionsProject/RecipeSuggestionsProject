using AutoMapper;
using RecipeSuggestions.Server.Models;
using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Interfaces;

namespace RecipeSuggestions.Server.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientDTO, Ingredient>();

            CreateMap<Ingredient_Recipe, IngredientWithQuantity>();
        }
    }
}