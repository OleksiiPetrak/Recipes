using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using RecipesBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipesBook.Core.ViewModels
{
    public class RecipeViewModel: BaseViewModel<Recipe, Recipe>
    {
        private readonly IMvxNavigationService _navigationService;

        public RecipeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Recipe _recipe;
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

        public TaskCompletionSource<object> CloseCompletionSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MvxNotifyTask InitializeTask { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Prepare(TParameter parameter)
        {
            throw new NotImplementedException();
        }

        public void ViewCreated()
        {
            throw new NotImplementedException();
        }

        public void ViewAppearing()
        {
            throw new NotImplementedException();
        }

        public void ViewAppeared()
        {
            throw new NotImplementedException();
        }

        public void ViewDisappearing()
        {
            throw new NotImplementedException();
        }

        public void ViewDisappeared()
        {
            throw new NotImplementedException();
        }

        public void ViewDestroy(bool viewFinishing = true)
        {
            throw new NotImplementedException();
        }

        public void Init(IMvxBundle parameters)
        {
            throw new NotImplementedException();
        }

        public void ReloadState(IMvxBundle state)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void SaveState(IMvxBundle state)
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Prepare(Recipe parameter)
        {
            throw new NotImplementedException();
        }
    }
}
