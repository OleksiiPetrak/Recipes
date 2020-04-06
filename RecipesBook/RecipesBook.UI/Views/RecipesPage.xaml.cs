using MvvmCross.Forms.Views;
using RecipesBook.Core.ViewModels;
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
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class RecipesPage : MvxContentPage<RecipesViewModel>
	{
		public RecipesPage ()
		{
			InitializeComponent ();
		}
	}
}