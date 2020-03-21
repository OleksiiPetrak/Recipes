using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var recipes = await _unitOfWork.Recipes.GetAllAsync();

            foreach(var recipe in recipes)
            {
                recipe.Ingredients = await _unitOfWork.Ingredients.FindAsync(x => x.RecipeId == recipe.Id);
                var ID = recipe.Id;
            }

            return recipes;
        }

        public async Task UpserOneRecipe(Recipe recipe, List<Ingredient> ingredients)
        {
            try
            {
                recipe.Id = Guid.NewGuid();
                await _unitOfWork.Recipes.UpsertOneAsync(recipe);

                foreach(var ingredient in ingredients)
                {
                    ingredient.RecipeId = recipe.Id;
                }

                await _unitOfWork.Ingredients.UpsertManyAsync(ingredients);
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
