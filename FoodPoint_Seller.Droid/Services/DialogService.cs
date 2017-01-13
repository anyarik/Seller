using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.Services.Implementations;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System;
using static Android.Resource;
using FoodPoint_Seller.Core.Models;

namespace FoodPoint_Seller.Droid.Services
{
    /// <summary>
    /// Provides a service to any code that requires a dialog to be shown to the user for more complex situations
    /// where responses and richer user interaction is required use the IInteractionRequest service.
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>Alerts the user with a simple OK dialog and provides a <paramref name="message"/>.</summary>
        /// <param name="message">The message.</param>
        /// <param name="title">The title.</param>
        /// <param name="okbtnText">The okbtn text.</param>
        /// 
        private event EventHandler<NotificaiosModel> NotificateIt;

        event EventHandler<NotificaiosModel> IDialogService.NotificateIt
        {
            add
            {
                this.NotificateIt += value;
            }

            remove
            {
                this.NotificateIt -= value;
            }
        }

        public void Alert(string message, string title, string okbtnText)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetIcon(Resource.Drawable.Icon);
            adb.SetPositiveButton(okbtnText, (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }

        public void Notification(NotificaiosModel notification)
        {
               this.NotificateIt.Invoke(null, notification);

        }
    }
}