using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace FoodPoint_Seller.Droid
{
    [Activity(
		Label = "FP продавец"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/AppTheme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Landscape)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.splash_screen)
        {
            UserDialogs.Init(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
        }
    }
}