using System;
using MvvmCross.Binding.iOS.Views;
using Foundation;
using UIKit;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using FoodPoint_Seller.Core.Models;
using CoreAnimation;
using FoodPoint_Seller.Touch.Views.Home;
using FoodPoint_Seller.Touch.Views.Home.ProductTables.ProductCell;

namespace Collections.Touch
{
    public partial class OrderCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName("OrderCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("OrderCell");

        public MvxSimpleTableViewSource source;

        public OrderCell(IntPtr handle) 
			: base (string.Empty /* TODO - this isn't really needed - mvx bug */, handle)
		{
            this.DelayBind(() => {
                source = new ProductTableSource(ProductTabel); //new ProductTableSource(ProductTabel);

                ProductTabel.Source = source;

                var set = this.CreateBindingSet<OrderCell, PayedOrder>();
                set.Bind(RowLabel).To(order => order.Order.RowNumber);
                set.Bind(Timerlbl).To(order => order.CloseOrderTimer.WaitTime).WithConversion("StringFormat", "mm\\:ss");
                set.Bind(source).To(order => order.Order.OrderedFood);
                
                set.Bind(OverOrderBtn).To("OnFinishOrder");
                set.Bind(OverOrderBtn).For("Visibility").To(order => order.IsOrderFinished)
                                                                         .WithConversion("Visibility");

                set.Apply();
                ProductTabel.ReloadData();
            });
        }
      

        public static OrderCell Create()
        {
            return (OrderCell)Nib.Instantiate(null, null)[0];
        }
        
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this.LayoutMargins = new UIEdgeInsets(10, 10, 10, 10);
            //this.Layer.SublayerTransform. = new CATransform3D();
           
            this.ContentView.Layer.CornerRadius = 2;
            this.ContentView.Layer.BorderWidth = 1;
            this.ContentView.Layer.BorderColor = UIColor.Clear.CGColor;
            this.ContentView.Layer.MasksToBounds = true;

            this.Layer.ShadowColor = UIColor.LightGray.CGColor;
            this.Layer.ShadowOffset = new CGSize(0, 2);
            this.Layer.ShadowRadius = 2;
            this.Layer.ShadowOpacity = 1;
            this.Layer.MasksToBounds = false;

            // this.Layer.ShadowPath = new UIBezierPath( this.Bounds,  this.ContentView.Layer.CornerRadius);
        }
    }
}