﻿// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FoodPoint_Seller.Touch.Views.Home
{
    [Register ("ProductCell")]
    partial class ProductCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView AdditiveView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NameLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AdditiveView != null) {
                AdditiveView.Dispose ();
                AdditiveView = null;
            }

            if (NameLbl != null) {
                NameLbl.Dispose ();
                NameLbl = null;
            }
        }
    }
}