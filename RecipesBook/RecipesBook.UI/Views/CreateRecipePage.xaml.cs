using MvvmCross.Forms.Views;
using RecipesBook.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace RecipesBook.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRecipePage : MvxContentPage<RecipeViewModel>
    {
        public CreateRecipePage()
        {
            InitializeComponent();
        }
    }
}