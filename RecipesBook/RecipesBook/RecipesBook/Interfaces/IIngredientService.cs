using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipesBook.Core.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetAllIngredients();
        Task<IEnumerable<Ingredient>> GetManyIngredients(Expression<Func<Ingredient, bool>> predicate);
        Task UpserOneIngredient(Ingredient ingredient);
        Task UpserManyIngredients(List<Ingredient> ingredients);
    }
}
