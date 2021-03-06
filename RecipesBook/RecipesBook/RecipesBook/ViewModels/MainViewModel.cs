﻿using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RecipesBook.Core.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand ShowRecipesViewModel { get; private set; }

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowRecipesViewModel = new MvxAsyncCommand(async () => await _navigationService.Navigate<RecipesViewModel>());
        }
    }
}
