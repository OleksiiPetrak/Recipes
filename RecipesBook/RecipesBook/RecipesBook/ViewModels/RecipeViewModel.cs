using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RecipesBook.Common.Enums;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using RecipesBook.Common.Extensions;

namespace RecipesBook.Core.ViewModels
{
    public class RecipeViewModel : BaseViewModel<Recipe, Recipe>
    {
        private readonly IMvxNavigationService _navigationService;
        private Category _categories;

        public RecipeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            DownloadPhotoButtonText = "Download photo";
            PickPhotoCommand = new MvxAsyncCommand(PickPhoto);
        }

        private string _cookingStreps;
        private ImageSource _recipeImageSource;
        private string _downloadPhotoButtonText;
        private string _selectedCategory;
        private Recipe _recipe;

        public List<string> Categories
        {
            get
            {
                return Enum.GetNames(typeof(Category)).Select(c => c.SplitCamelCase()).ToList();
            }
        }

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

        public ImageSource RecipeImageSource
        {
            get
            {
                return _recipeImageSource;
            }

            set
            {
                _recipeImageSource = value;
                RaisePropertyChanged(() => RecipeImageSource);
            }
        }

        public string SelectedCategory 
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
            }
        }

        public string DownloadPhotoButtonText
        {
            get
            {
                return _downloadPhotoButtonText;
            }
            set
            {
                _downloadPhotoButtonText = value;
                RaisePropertyChanged(() => DownloadPhotoButtonText);
            }
        }

        public string CookingSteps
        {
            get
            {
                return _cookingStreps;
            }

            set
            {
                _cookingStreps = value;
                RaisePropertyChanged(() => CookingSteps);
            }
        }

        public IMvxCommand PickPhotoCommand { get; private set; }

        public override void Prepare(Recipe parameter)
        {
            Recipe = parameter;
        }

        private async Task PickPhoto()
        {
            await CheckPermisionsAsync();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                _downloadPhotoButtonText = "Photos Not Supported(";
                //await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
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
                // Xamarin.Insights.Report(ex);
                // await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured it in Xamarin Insights! Thanks.", "OK");
            }   
        }

        private async Task CheckPermisionsAsync()
        {
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (storageStatus != PermissionStatus.Granted)
            {
                var permissions = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                storageStatus = permissions[Permission.Storage];
            }
        }
    }
}
