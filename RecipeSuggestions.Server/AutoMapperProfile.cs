
using AutoMapper;
using RecipeSuggestions.Server.Models;
using RecipeSuggestions.Server.DTOs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Ingredient, IngredientDTO>(); 
        CreateMap<IngredientDTO, Ingredient>(); 
    }
}
