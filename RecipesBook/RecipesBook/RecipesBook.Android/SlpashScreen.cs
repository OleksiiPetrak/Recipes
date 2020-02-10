using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Forms.Platforms.Android.Views;
using RecipesBook.UI;

namespace RecipesBook.Droid
{
    [Activity(Label = "SlpashScreen")]
    public class SlpashScreen : MvxFormsSplashScreenActivity<Setup, Core.App, App>
    {
        public SlpashScreen(): base(Resource.Layout.SplashScreen)
        {
        }
    }
}