using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeSuggestions.Server.Data.Interfaces;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Data
{
    public class RecipeSuggestionsServerContext : DbContext, IRecipeSuggestionsServerContext
    {
        public RecipeSuggestionsServerContext (DbContextOptions<RecipeSuggestionsServerContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeSuggestions.Server.Models.Recipe> Recipe { get; set; } = default!;
    }
}
