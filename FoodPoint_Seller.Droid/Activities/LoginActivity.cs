using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views.InputMethods;
using FoodPoint_Seller.Core.ViewModels;
using FoodPoint_Seller.Droid.Adapter;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace FoodPoint_Seller.Droid.Activities
{
    [Activity(
        Label = "Examples",
        Theme = "@style/AppTheme.Login",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize,
        ScreenOrientation = ScreenOrientation.Landscape
    )]			
    public class LoginActivity : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            UserDialogs.Init(this);
            SetContentView(Resource.Layout.activity_login);

            var view = this.FindViewById<MvxSpinner>(Resource.Id.role_spinner);
            view.Adapter = new SpinerRoleAdapter(this, (IMvxAndroidBindingContext)BindingContext);
            view.SetSelection(2);
            // this.HideSoftKeyboard();
        }

     
        //public void HideSoftKeyboard()
        //{
        //    if (CurrentFocus == null) return;

        //    InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
        //    inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

        //    CurrentFocus.ClearFocus();
        //}
    }
}

