using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesBook.Core.Services
{
    public class IngredientsService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Ingredient>> GetIngredients()
        {
            var ingredients = await _unitOfWork.Ingredients.GetAllAsync();

            return ingredients;
        }

        public async Task UpserManyIngredients(List<Ingredient> ingredients)
        {
            await _unitOfWork.Ingredients.UpsertManyAsync(ingredients);
        }

        public async Task UpserOneIngredient(Ingredient ingredient)
        {
            try
            {
                await _unitOfWork.Ingredients.UpsertOneAsync(ingredient);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
