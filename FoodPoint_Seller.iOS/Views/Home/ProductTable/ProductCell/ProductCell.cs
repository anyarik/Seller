using System;
using Foundation;
using UIKit;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;

namespace FoodPoint_Seller.Touch.Views.Home.ProductTables.ProductCell
{
    public partial class ProductCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName("ProductCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("ProductCell");

        public ProductCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {

                var set = this.CreateBindingSet<ProductCell, ProductForOrder>();
                set.Bind(NameLbl).To(product => product.ProductInfo.Name);

                set.Apply();
            });
        }


        public static ProductCell Create()
        {
            return (ProductCell)Nib.Instantiate(null, null)[0];
        }
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
        }
    }
}