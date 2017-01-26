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
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services;

namespace FoodPoint_Seller.Droid.Services
{
    class UpdatingService:IUpdatingService
    {
        private event EventHandler<NotificaiosModel> NotificateIt;

        event EventHandler<NotificaiosModel> IUpdatingService.NotificateIt
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

        public void Update()
        {
        }
    }
}