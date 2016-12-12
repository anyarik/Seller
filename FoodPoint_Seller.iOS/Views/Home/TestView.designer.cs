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

namespace FoodPoint_Seller.Touch.Views.Home
{
    [Register ("TestView")]
    partial class TestView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CountCancelOrderLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CountDoneOrderLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CountNotAnswerOrderLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ListOrderItemTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CountCancelOrderLabel != null) {
                CountCancelOrderLabel.Dispose ();
                CountCancelOrderLabel = null;
            }

            if (CountDoneOrderLabel != null) {
                CountDoneOrderLabel.Dispose ();
                CountDoneOrderLabel = null;
            }

            if (CountNotAnswerOrderLabel != null) {
                CountNotAnswerOrderLabel.Dispose ();
                CountNotAnswerOrderLabel = null;
            }

            if (ListOrderItemTable != null) {
                ListOrderItemTable.Dispose ();
                ListOrderItemTable = null;
            }
        }
    }
}