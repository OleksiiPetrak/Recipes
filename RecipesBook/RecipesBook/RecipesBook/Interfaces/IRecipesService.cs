using RecipesBook.Common.Enums;
using RecipesBook.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesBook.Core.Interfaces
{
    public interface IRecipesService
    {
        Task<IEnumerable<Recipe>> GetRecipes();
        Task<IEnumerable<Recipe>> GetRecipesByCategory(Category category);
        Task UpserOneRecipe(Recipe recipe, List<Ingredient> ingredients);
        Task UpserManyRecipes(List<Recipe> recipes);
    }
}
