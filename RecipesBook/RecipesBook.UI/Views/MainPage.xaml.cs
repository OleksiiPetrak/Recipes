using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using RecipesBook.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipesBook.UI.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, NoHistory = true)]
    public partial class MainPage : MvxMasterDetailPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.ShowRecipesViewModel.Execute(null);

            base.OnAppearing();
        }
    }
}