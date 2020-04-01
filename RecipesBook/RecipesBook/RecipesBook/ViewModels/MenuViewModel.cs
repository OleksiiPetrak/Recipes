using MvvmCross.Commands;
using MvvmCross.Navigation;
using RecipesBook.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipesBook.Core.ViewModels
{
    public class MenuViewModel: BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowFirstDishesCommand = new MvxAsyncCommand(async () => 
            await _navigationService.Navigate<RecipesViewModel>(ConvertEnumToString(Category.FirstDish)));
            ShowMainDishesCommand = new MvxAsyncCommand(async () =>
            await _navigationService.Navigate<RecipesViewModel>(ConvertEnumToString(Category.MainDish)));
            ShowSaladsCommand = new MvxAsyncCommand(async () =>
            await _navigationService.Navigate<RecipesViewModel>(ConvertEnumToString(Category.Salad)));
            ShowDessertsCommand = new MvxAsyncCommand(async () =>
            await _navigationService.Navigate<RecipesViewModel>(ConvertEnumToString(Category.Dessert)));
            ShowCocktailsCommand = new MvxAsyncCommand(async () =>
            await _navigationService.Navigate<RecipesViewModel>(ConvertEnumToString(Category.Cocktail)));
        }

        public IMvxCommand ShowFirstDishesCommand { get; private set; }
        public IMvxCommand ShowMainDishesCommand { get; private set; }
        public IMvxCommand ShowSaladsCommand { get; private set; }
        public IMvxCommand ShowDessertsCommand { get; private set; }
        public IMvxCommand ShowCocktailsCommand { get; private set; }

        private string ConvertEnumToString(Category category)
        {
            var result = Enum.GetName(typeof(Category), category);
            return result;
        }
    }
}
