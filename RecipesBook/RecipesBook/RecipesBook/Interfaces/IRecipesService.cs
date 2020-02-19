using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipesBook.Core.Interfaces
{
    public interface IRecipesService
    {
        Task<IEnumerable<Recipe>> GetRecipes();
        Task AddNewRecipes(Recipe recipe);
    }
}
