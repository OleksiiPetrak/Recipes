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
    public class RecipesViewModel: BaseViewModel
    {
        private string _nextPage;
        private readonly IMvxNavigationService _navigationService;
        private readonly IRecipesService _recipesService;

        public RecipesViewModel(IRecipesService recipesService,
            IMvxNavigationService navigationService)
        {
             _recipesService = recipesService;
             _navigationService = navigationService;

             Recipes = new MvxObservableCollection<Recipe>();

            RecipeSelectedCommand = new MvxAsyncCommand<Recipe>(RecipeSelected);
            FetchRecipesCommand = new MvxCommand(
                () =>
                {
                    if (!string.IsNullOrEmpty(_nextPage))
                    {
                        FetchRecipesTask = MvxNotifyTask.Create(LoadRecipes);
                        RaisePropertyChanged(() => FetchRecipesTask);
                    }
                });
            RefreshRecipesCommand = new MvxCommand(RefreshRecipes);
        }

        public override Task Initialize()
        {
            LoadRecipesTask = MvxNotifyTask.Create(LoadRecipes);
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

        private async Task LoadRecipes()
        {
            var result = await _recipesService.GetRecipes();
        }

        private async Task RecipeSelected(Recipe selectedRecipe)
        {
            await _navigationService.Navigate<RecipeViewModel, Recipe>(selectedRecipe);
        }

        private void RefreshRecipes()
        {
            _nextPage = null;

            LoadRecipesTask = MvxNotifyTask.Create(LoadRecipes);

            RaisePropertyChanged(() => LoadRecipesTask);
        }
    }
}
