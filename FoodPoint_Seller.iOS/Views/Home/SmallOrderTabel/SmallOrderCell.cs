using CoreGraphics;
using FoodPoint_Seller.Core.Models;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using System;

using UIKit;

namespace FoodPoint_Seller.Touch.Views.Home
{
    public partial class SmallOrderCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName("SmallOrderCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("SmallOrderCell");

        public SmallOrderCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SmallOrderCell, PayedOrder>();
                set.Bind(RowLbl).To(order => order.Order.RowNumber);
                set.Bind(TimerLbl).To(order => order.CloseOrderTimer.WaitTime)
                                  .WithConversion("StringFormat", "mm\\:ss"); 
                set.Apply();
            });
        }

        public static SmallOrderCell Create()
        {
            return (SmallOrderCell)Nib.Instantiate(null, null)[0];
        }
    }
}