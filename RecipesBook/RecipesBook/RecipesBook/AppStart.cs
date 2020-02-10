using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RecipesBook.Core.ViewModels;
using System.Threading.Tasks;

namespace RecipesBook.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
            : base(app, mvxNavigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<MainViewModel>();
        }
    }
}
