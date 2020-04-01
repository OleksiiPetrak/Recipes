using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RecipesBook.Common.Enums;
using RecipesBook.Core.Interfaces;
using RecipesBook.Core.Models;
using System.Threading.Tasks;

namespace RecipesBook.Core.ViewModels
{
    public class RecipesViewModel : BaseViewModel<string>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRecipesService _recipesService;
        private readonly Category category;

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

        private async Task LoadRecipes()
        {
            var recipesList = await _recipesService.GetRecipes();
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

        public override void Prepare(string parameter)
        {
            
        }
    }
}
