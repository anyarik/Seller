
using System;
using System.Drawing;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using FoodPoint_Seller.Api.Models.ViewModels;

namespace FoodPoint_Seller.Touch.Views.Home.AdditiveTable.AdditiveCell
{
    public partial class AdditiveCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName("AdditiveCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("AdditiveCell");

       public AdditiveCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {

                var set = this.CreateBindingSet<AdditiveCell, AdditiveForProduct>();
                set.Bind(NameLbl).To(additive => additive.AdditiveName);

                set.Apply();
            });
        }


        public static AdditiveCell Create()
        {
            return (AdditiveCell)Nib.Instantiate(null, null)[0];
        }
    }
}