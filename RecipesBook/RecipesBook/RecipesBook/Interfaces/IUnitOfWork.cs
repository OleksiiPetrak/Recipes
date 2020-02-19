using RecipesBook.Core.Models;

namespace RecipesBook.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryAsync<Ingredient> Ingredients {get;}
        IRepositoryAsync<Recipe> Recipes { get; }
    }
}
