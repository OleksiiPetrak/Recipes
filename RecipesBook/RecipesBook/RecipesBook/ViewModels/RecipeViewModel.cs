using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RecipesBook.Common.Enums;
using RecipesBook.Common.Extensions;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using RecipesBook.Core.Interfaces;

namespace RecipesBook.Core.ViewModels
{
    public class RecipeViewModel : BaseViewModel<Recipe, Recipe>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRecipesService _recipesService;

        public RecipeViewModel(IMvxNavigationService navigationService, 
            IRecipesService recipesService)
        {
            _navigationService = navigationService;
            _recipesService = recipesService;
            InitializeIngredientCollections();
            RecipeImageSource = ImageSource.FromFile("cookieEmptyPhoto.png");
            DownloadPhotoButtonText = "Download photo";
            SaveRecipeButtonText = "Save recipe";
            CookingTimeText = "Select cooking time";
            PickPhotoCommand = new MvxAsyncCommand(PickPhoto);
            AddIngredientCommand = new MvxAsyncCommand(AddIngredient);
            RefreshIngredientsCommand = new MvxCommand(RefreshIngredients);
            SaveRecipeCommand = new MvxAsyncCommand(SaveRecipe);
        }

        private string _recipeName;
        private string _cookingStreps;
        private string _cookingTimeText;
        private ImageSource _recipeImageSource;
        private string _downloadPhotoButtonText;
        private string _saveRecipeButtonText;
        private string _selectedCategory;
        private int _cookingTime;
        private Recipe _recipe;
        private List<Ingredient> _ingredients;
        private MvxObservableCollection<Ingredient> _ingredientsToList;

        public List<string> Categories
        {
            get
            {
                return Enum.GetNames(typeof(Category)).Select(c => c.SplitCamelCase()).ToList();
            }
        }

        public MvxObservableCollection<Ingredient> Ingredients
        {
            get => _ingredientsToList;
            set
            {
                _ingredientsToList = value;
                RaisePropertyChanged(() => Ingredients);
            }
        }

        public Recipe Recipe
        {
            get => _recipe;
            set
            {
                _recipe = value;
                RaisePropertyChanged(() => Recipe);
            }
        }

        public string RecipeName
        {
            get => _recipeName;
            set
            {
                _recipeName = value;
                RaisePropertyChanged(() => RecipeName);
            }
        }

        public int CookingTime
        {
            get => _cookingTime;
            set
            {
                _cookingTime = value;
                RaisePropertyChanged(() => CookingTime);

                OutputCookingTime();
            }
        }

        public string CookingTimeText
        {
            get => _cookingTimeText;
            set
            {
                _cookingTimeText = value;
                RaisePropertyChanged(() => CookingTimeText);
            }
        }

        public ImageSource RecipeImageSource
        {
            get => _recipeImageSource;
            set
            {
                _recipeImageSource = value;
                RaisePropertyChanged(() => RecipeImageSource);
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
            }
        }

        public string DownloadPhotoButtonText
        {
            get => _downloadPhotoButtonText;
            set
            {
                _downloadPhotoButtonText = value;
                RaisePropertyChanged(() => DownloadPhotoButtonText);
            }
        }

        public string SaveRecipeButtonText
        {
            get => _saveRecipeButtonText;
            set
            {
                _saveRecipeButtonText = value;
                RaisePropertyChanged(() => SaveRecipeButtonText);
            }
        }

        public string CookingSteps
        {
            get => _cookingStreps;
            set
            {
                _cookingStreps = value;
                RaisePropertyChanged(() => CookingSteps);
            }
        }

        public IMvxCommand PickPhotoCommand { get; private set; }
        public IMvxCommand AddIngredientCommand { get; private set; }
        public IMvxCommand RefreshIngredientsCommand { get; private set; }
        public IMvxCommand SaveRecipeCommand { get; private set; }

        private async Task PickPhoto()
        {
            await CheckPermisionsAsync();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                _downloadPhotoButtonText = "Photos Not Supported(";
                return;
            }
            try
            {
                Stream stream = null;
                var file = await CrossMedia.Current.PickPhotoAsync().ConfigureAwait(true);


                if (file == null)
                    return;

                stream = file.GetStream();
                file.Dispose();

                RecipeImageSource = ImageSource.FromStream(() => stream);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddIngredient()
        {
            _ingredients = await _navigationService.Navigate<IngredientViewModel,
                List<Ingredient>, List<Ingredient>>(_ingredients).ConfigureAwait(false);
            RefreshIngredients();
        }

        private async Task CheckPermisionsAsync()
        {
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage).ConfigureAwait(false);

            if (storageStatus != PermissionStatus.Granted)
            {
                var permissions = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage).ConfigureAwait(false);
                storageStatus = permissions[Permission.Storage];
            }
        }

        private void RefreshIngredients()
        {
            Ingredients.Clear();
            Ingredients.AddRange(_ingredients);
        }

        private void InitializeIngredientCollections()
        {
            if (_ingredients == null)
            {
                _ingredients = new List<Ingredient>();
            }
            if (Ingredients == null)
            {
                Ingredients = new MvxObservableCollection<Ingredient>();
            }
        }

        private void OutputCookingTime()
        {
            CookingTimeText = $"Will cook {CookingTime} min";
        }

        private Category ConverCategoryInEnum(string name)
        {
            var consistentName = name.Replace(" ", "");
            var category = (Category)Enum.Parse(typeof(Category), consistentName);
            return category;
        }

        public async Task SaveRecipe()
        {
            if (RecipeName != null && RecipeImageSource != null && CookingTime != 0 &&
                SelectedCategory != null && Ingredients != null && CookingSteps != null)
            {
                try
                {
                    var category = ConverCategoryInEnum(SelectedCategory);

                    var recipe = new Recipe
                    {
                        Title = RecipeName,
                        RecipeImage = RecipeImageSource.ToString(),
                        CookingTime = CookingTime,
                        Category = category,
                        Ingredients = _ingredients,
                        CookingSteps = CookingSteps
                    };

                    await _recipesService.UpserOneRecipe(recipe, _ingredients);

                    await _navigationService.Navigate<RecipesViewModel>().ConfigureAwait(false);
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            else
            {
                SaveRecipeButtonText = "Input all correct data";
            }
        }

        public override void Prepare(Recipe parameter)
        {
            Recipe = parameter;
        }
    }
}
