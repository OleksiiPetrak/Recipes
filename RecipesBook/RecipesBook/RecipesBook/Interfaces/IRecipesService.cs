using RecipesBook.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesBook.Core.Interfaces
{
    public interface IRecipesService
    {
        Task<IEnumerable<Recipe>> GetRecipes();
        Task UpserOneRecipe(Recipe recipe, List<Ingredient> ingredients);
        Task UpserManyRecipes(List<Recipe> recipes);
    }
}
