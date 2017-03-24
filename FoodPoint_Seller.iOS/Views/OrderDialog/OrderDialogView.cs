using System;
using CoreGraphics;
using UIKit;

namespace FoodPoint_Seller.Touch.Views.OrderDialog
{
    public partial class OrderDialogView : UIViewController
    {
        private CGRect cGRect;

        public OrderDialogView() : base("OrderDialogView", null)
        {
        }

        public OrderDialogView(CGRect cGRect)
        {
            this.View.Bounds = cGRect;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}