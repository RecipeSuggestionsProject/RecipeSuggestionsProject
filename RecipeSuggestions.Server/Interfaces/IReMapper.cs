using RecipeSuggestions.Server.DTOs;
using RecipeSuggestions.Server.Models;

namespace RecipeSuggestions.Server.Interfaces
{
    public interface IReMapper<Source, Destination>
    {
        public Destination Map(Source sourceObject);
        public IEnumerable<Destination> Map(IEnumerable<Source> sourceObjects)
        {
            var destinationObjects = new List<Destination>();

            foreach (var sourceObject in sourceObjects) {
                destinationObjects.Add(Map(sourceObject));
            }

            return destinationObjects;
        }
    }
}
