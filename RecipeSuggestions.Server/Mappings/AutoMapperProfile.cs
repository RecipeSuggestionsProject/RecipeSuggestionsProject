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

            CreateMap<RecipeWithIngredients, RecipeDTO>()
                .IncludeMembers(src => src.Recipe)
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients_Recipes))
                .AfterMap((src, dest) => {
                    if (src.Recipe == null || src.Ingredients_Recipes == null) { return; }
                    foreach (var ingredient_recipe in src.Ingredients_Recipes)
                    {
                        if (ingredient_recipe != null)
                        {
                            ingredient_recipe.RecipeId = src.Recipe.Id;
                            ingredient_recipe.Recipe = src.Recipe;
                            if (ingredient_recipe.Ingredient != null)
                            {
                                ingredient_recipe.IngredientId = ingredient_recipe.Ingredient.Id;
                            }
                        }  
                    }
                })
                .ReverseMap()
            ;

            CreateMap<RecipeDTO, Recipe>();
            CreateMap<Recipe, RecipeDTO>().AfterMap<SetRecipeDTOIngredientsAction>();

            CreateMap<Ingredient_Recipe, IngredientWithQuantity>().ReverseMap();
        }
    }
}