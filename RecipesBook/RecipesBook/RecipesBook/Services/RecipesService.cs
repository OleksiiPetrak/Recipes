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
    }
}
