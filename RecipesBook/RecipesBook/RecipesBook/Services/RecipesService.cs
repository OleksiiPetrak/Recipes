using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;

namespace RecipesBook.Core.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IRepositoryAsync<Recipe> _recipeRepository;
        public RecipesService(IRepositoryAsync<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Task<IEnumerable<Recipe>> GetRecipes()
        {
            throw new NotImplementedException();
        }
    }
}
