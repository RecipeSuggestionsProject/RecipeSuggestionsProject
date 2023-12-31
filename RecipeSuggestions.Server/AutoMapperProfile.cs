
using AutoMapper;
using RecipeSuggestions.Server.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Ingredient, IngredientDTO>(); 
        CreateMap<IngredientDTO, Ingredient>(); 
    }
}
