using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RecipeSuggestions.Server.Data.Interfaces
{
    public interface IRecipeSuggestionsServerContext
    {
        public DbSet<RecipeSuggestions.Server.Models.Recipe> Recipe { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public EntityEntry Entry(object entity);
    }
}
