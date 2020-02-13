using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipesBook.Core.ViewModels
{
    public class RecipesViewModel: MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRecipesService _recipesService;

        public RecipesViewModel(IRecipesService recipesService,
            IMvxNavigationService navigationService)
        {
            _recipesService = recipesService;
            _navigationService = navigationService;

            Recipes = new MvxObservableCollection<Recipe>();
        }

        public override Task Initialize()
        {


            return base.Initialize();
        }

        public MvxNotifyTask LoadRecipesTask { get; private set; }
        public MvxNotifyTask FetchRecipesTask { get; private set; }


        private MvxObservableCollection<Recipe> _recipes;

        public MvxObservableCollection<Recipe> Recipes
        {
            get
            {
                return _recipes;
            }
            set
            {
                _recipes = value;
                RaisePropertyChanged(() => Recipes);
            }
        }

        public IMvxCommand<Recipe> RecipeSelectedCommand { get; private set; }
        public IMvxCommand FetchRecipesCommand { get; private set; }
        public IMvxCommand RefreshRecipesCommand { get; private set; }

        private async Task RecipeSelected(Recipe selectedRecipe)
        {
            var result = await _navigationService.Navigate<RecipeViewModel, Recipe>();
        }
    }
}
