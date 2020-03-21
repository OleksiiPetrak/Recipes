using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipesBook.Core.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetIngredients();
        Task UpserOneIngredient(Ingredient ingredient);
        Task UpserManyIngredients(List<Ingredient> ingredients);
    }
}
