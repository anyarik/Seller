using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace FoodPoint_Seller.Droid
{
    [Activity(
		Label = "FoodPoint_Seller.Droid"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/AppTheme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Landscape)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.splash_screen)
        {
        }
    }
}