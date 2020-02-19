using RecipesBook.Common.Interfaces;
using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
using SQLite;

namespace RecipesBook.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SQLiteAsyncConnection _connection;
        private IRepositoryAsync<Ingredient> _ingredients;
        private IRepositoryAsync<Recipe> _recipes;

        public UnitOfWork(IFileAccessHelper fileAccessHelper)
        {
            string dbPath = fileAccessHelper.GetLocalFilePath("recipes.db3");
            _connection = new SQLiteAsyncConnection(dbPath);
        }

        public IRepositoryAsync<Ingredient> Ingredients => 
            _ingredients ?? (_ingredients = new GenericRepositoryAsync<Ingredient>(_connection));

        public IRepositoryAsync<Recipe> Recipes =>
            _recipes ?? (_recipes = new GenericRepositoryAsync<Recipe>(_connection));
    }
}
