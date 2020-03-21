using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesBook.Core.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            var resipes = await _unitOfWork.Recipes.GetAllAsync();

            return resipes;
        }

        public async Task UpserOneRecipe(Recipe recipe)
        {
            try
            {
                await _unitOfWork.Recipes.UpsertOneAsync(recipe);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpserManyRecipes(List<Recipe> recipes)
        {
            await _unitOfWork.Recipes.UpsertManyAsync(recipes);
        }
    }
}
