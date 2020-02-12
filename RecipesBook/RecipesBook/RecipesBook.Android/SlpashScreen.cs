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
using Android.Content.PM;
using System.Threading.Tasks;

namespace RecipesBook.Droid
{
    [Activity(
        Label = "SlpashScreen", 
        Theme = "@style/MainTheme.Splash", 
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SlpashScreen : MvxFormsSplashScreenActivity<Setup, Core.App, App>
    {
        public SlpashScreen(): base(Resource.Layout.SplashScreen)
        {
        }

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(RootActivity));
            return Task.CompletedTask;
        }
    }
}