using System;
using Foundation;
using UIKit;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;
using FoodPoint_Seller.Touch.Views.Home.AdditiveTable.AdditiveCell;

namespace FoodPoint_Seller.Touch.Views.Home.ProductTables.ProductCell
{
    public partial class ProductCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName("ProductCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("ProductCell");

        // public MvxSimpleTableViewSource source;
        public UITableView additiveTableView;

        public ProductCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                additiveTableView = new UITableView(new CGRect(0, 0, AdditiveView.Bounds.Width, AdditiveView.Bounds.Height));
                AdditiveView.AddSubview(additiveTableView);
                additiveTableView.RegisterClassForCellReuse(typeof(AdditiveCell), AdditiveCell.Key);
                var source = new MvxSimpleTableViewSource(additiveTableView, AdditiveCell.Key, AdditiveCell.Key);
                additiveTableView.Source = source;

                var set = this.CreateBindingSet<ProductCell, ProductForOrder>();
                set.Bind(NameLbl).To(product => product.ProductInfo.Name);
                set.Bind(source).To(product => product.ProductInfo.OrderedAdditives);

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
            additiveTableView.EstimatedRowHeight = 50;
            additiveTableView.RowHeight = 50;

        }
    }
}