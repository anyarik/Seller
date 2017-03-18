// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables
{
    [Register ("OrderCell")]
    partial class OrderCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton OverOrderBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RowLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView TestView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Timerlbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (OverOrderBtn != null) {
                OverOrderBtn.Dispose ();
                OverOrderBtn = null;
            }

            if (RowLabel != null) {
                RowLabel.Dispose ();
                RowLabel = null;
            }

            if (TestView != null) {
                TestView.Dispose ();
                TestView = null;
            }

            if (Timerlbl != null) {
                Timerlbl.Dispose ();
                Timerlbl = null;
            }
        }
    }
}