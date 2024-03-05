using AutoMapper;
using RecipeSuggestions.Server.Models;
using RecipeSuggestions.Server.DTOs;

namespace RecipeSuggestions.Server.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientDTO, Ingredient>();

            CreateMap<RecipeWithIngredients, RecipeDTO>()
                .IncludeMembers(src => src.Recipe)
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients_Recipes))
                .AfterMap((src, dest) => {
                    if (src.Recipe == null || src.Ingredients_Recipes == null) { return; }
                    foreach (var ingredient_recipe in src.Ingredients_Recipes)
                    {
                        ingredient_recipe.RecipeId = src.Recipe.Id;
                        ingredient_recipe.Recipe = src.Recipe;
                        ingredient_recipe.IngredientId = ingredient_recipe.Ingredient.Id;
                    }
                })
                .ReverseMap()
            ;
            CreateMap<Recipe, RecipeDTO>().ReverseMap();
            CreateMap<Ingredient_Recipe, IngredientWithQuantity>().ReverseMap();
        }
    }
}