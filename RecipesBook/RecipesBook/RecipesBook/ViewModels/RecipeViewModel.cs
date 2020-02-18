using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipesBook.Core.ViewModels
{
    public class RecipeViewModel: BaseViewModel<Recipe, Recipe>
    {
        private readonly IMvxNavigationService _navigationService;

        public RecipeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Recipe _recipe;
        public Recipe Recipe
        {
            get
            {
                return _recipe;
            }

            set
            {
                _recipe = value;
                RaisePropertyChanged(() => Recipe);
            }
        }

        public override void Prepare(Recipe parameter)
        {
            Recipe = parameter;
        }
    }
}
