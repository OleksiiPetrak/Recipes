using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipesBook.Core.ViewModels
{
    public class MainViewModel:MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;


        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
