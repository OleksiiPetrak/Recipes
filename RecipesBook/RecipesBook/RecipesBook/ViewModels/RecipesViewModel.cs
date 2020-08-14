using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RecipesBook.Common.Enums;
using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesBook.Core.ViewModels
{
    public class RecipesViewModel : BaseViewModel<string>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRecipesService _recipesService;
        private string _category;

        public RecipesViewModel(IRecipesService recipesService,
            IMvxNavigationService navigationService)
        {
            if (_recipes == null)
            {
                _recipes = new MvxObservableCollection<Recipe>();
            }
            _recipesService = recipesService;
            _navigationService = navigationService;

            Recipes = new MvxObservableCollection<Recipe>();

            RecipeSelectedCommand = new MvxAsyncCommand<Recipe>(RecipeSelected);
            FetchRecipesCommand = new MvxCommand(
                () =>
                {
                    FetchRecipesTask = MvxNotifyTask.Create(LoadRecipes);
                    RaisePropertyChanged(() => FetchRecipesTask);
                });
            RefreshRecipesCommand = new MvxCommand(RefreshRecipes);
            NavigateToCreateRecipePageCommand = new MvxAsyncCommand(NavigateToCreateRecipePage);
        }

        public override Task Initialize()
        {
            LoadRecipesTask = MvxNotifyTask.Create(LoadRecipes);
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            RefreshRecipes();
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
        public IMvxCommand NavigateToCreateRecipePageCommand { get; private set; }

        public override void Prepare(string parameter)
        {
            _category = parameter;
        }

        private async Task LoadRecipes()
        {
            IEnumerable<Recipe> recipesList;

            if(_category == null)
            {
                recipesList = await _recipesService.GetRecipes();
            }
            else
            {
                var category = ConverCategoryInEnum(_category);
                recipesList = await _recipesService.GetRecipesByCategory(category);
            }

            if (recipesList != null)
            {
                Recipes.Clear();
                Recipes.AddRange(recipesList);
            }
        }

        private async Task RecipeSelected(Recipe selectedRecipe)
        {
            await _navigationService.Navigate<RecipeViewModel, Recipe>(selectedRecipe);
        }

        private void RefreshRecipes()
        {
            LoadRecipesTask = MvxNotifyTask.Create(LoadRecipes);

            RaisePropertyChanged(() => LoadRecipesTask);
        }

        private async Task NavigateToCreateRecipePage()
        {
            await _navigationService.Navigate<RecipeViewModel>();
        }

        private Category ConverCategoryInEnum(string name)
        {
            var consistentName = name.Replace(" ", "");
            var category = (Category)Enum.Parse(typeof(Category), consistentName);
            return category;
        }
    }
}
