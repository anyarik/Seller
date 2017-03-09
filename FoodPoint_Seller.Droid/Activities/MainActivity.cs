using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using FoodPoint_Seller.Core.ViewModels;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Controllers.Implementations;
using CrossUI.Droid.Dialog.Elements;
using MvvmCross.Binding.BindingContext;
using Android.Content;
using Android.Support.V4.App;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Droid.Services;
using Android.Media;

namespace FoodPoint_Seller.Droid.Activities
{
    [Activity(
        Label = "",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop,
        ScreenOrientation = ScreenOrientation.Landscape
    )]


    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        public DrawerLayout DrawerLayout;

        private static readonly int ButtonClickNotificationId = 1000;

        private IOrderController orderController;

        private IDialogService _dialogService;



        private void _dialogService_NotificateIt(object sender, NotificaiosModel notification)
        {
            // Pass the current button press count value to the next activity:
            Bundle valuesForActivity = new Bundle();


            // When the user clicks the notification, SecondActivity will start up.
            Intent resultIntent = new Intent(this, typeof(MainActivity));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);

            // Construct a back stack for cross-task navigation:
            Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            PendingIntent resultPendingIntent =
                stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

            // Build the notification:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
               
                .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
                .SetContentIntent(resultPendingIntent)  // Start 2nd activity when the intent is clicked.
                .SetContentTitle(notification.title)      // Set its title
                .SetNumber(1)                       // Display the count in the Content Info
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetVibrate(new long[] { 500, 500 })
                .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)  // Display this icon
                .SetContentText(notification.description); // The message to display.

            // Finally, publish the notification:
            NotificationManager notificationManager =
                (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(ButtonClickNotificationId, builder.Build());
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);

            this.orderController = Mvx.IocConstruct<OrderController>();


            this._dialogService = Mvx.Resolve<IDialogService>();


            _dialogService.NotificateIt += _dialogService_NotificateIt;

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);


            if (bundle == null)
                ViewModel.ShowMenu();
        }
    

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ShowBackButton()
        {
            //TODO Tell the toggle to set the indicator off
            //this.DrawerToggle.DrawerIndicatorEnabled = false;

            //Block the menu slide gesture
            DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
        }

        private void ShowHamburguerMenu()
        {
            //TODO set toggle indicator as enabled 
            //this.DrawerToggle.DrawerIndicatorEnabled = true;

            //Unlock the menu sliding gesture
            DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
                DrawerLayout.CloseDrawers();
            else
                base.OnBackPressed();
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null) return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //orderController.HubDisconnect();
        }
        public override void Finish()
        {
            base.Finish();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }     

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
