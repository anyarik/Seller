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
    [Register ("SmallOrderCell")]
    partial class SmallOrderCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RowLbl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TimerLbl { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (RowLbl != null) {
                RowLbl.Dispose ();
                RowLbl = null;
            }

            if (TimerLbl != null) {
                TimerLbl.Dispose ();
                TimerLbl = null;
            }
        }
    }
}