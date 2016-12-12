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

namespace FoodPoint_Seller.Touch.Views.Home.Cells
{
    [Register ("OrderCell")]
    partial class OrderCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel IdLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (IdLabel != null) {
                IdLabel.Dispose ();
                IdLabel = null;
            }
        }
    }
}