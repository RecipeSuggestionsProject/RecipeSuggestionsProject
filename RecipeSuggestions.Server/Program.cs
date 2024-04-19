using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeSuggestions.Server.Data;
using RecipeSuggestions.Server.Interfaces;
using RecipeSuggestions.Server.Services;
using RecipeSuggestions.Server.Mappings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RecipeSuggestionsServerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RecipeSuggestionsServerContext") ?? throw new InvalidOperationException("Connection string 'RecipeSuggestionsServerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRecipesService), typeof(RecipesService));
builder.Services.AddScoped(typeof(IIngredients_RecipesService), typeof(Ingredients_RecipesService));
builder.Services.AddScoped<IIngredientsService, IngredientsService>();
builder.Services.AddScoped<IRecipeMapper, RecipeMapper>();
builder.Services.AddScoped<IIngredientMapper, IngredientMapper>();
builder.Services.AddScoped<IRelationMapper, RelationMapper>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
