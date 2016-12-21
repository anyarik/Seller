// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using MvvmCross.iOS.Support.SidePanels;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace FoodPoint_Seller.Touch.Views
{
    [Register ("HomeView")]
    partial class HomeView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ApproveOrderButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CancelOrderButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView CurentOrderProductItemTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView DialogPanel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView ListOrderItemTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RecivedOrderNumberLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RecivedOrderTimeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RecivedOrderTimerLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ApproveOrderButton != null) {
                ApproveOrderButton.Dispose ();
                ApproveOrderButton = null;
            }

            if (CancelOrderButton != null) {
                CancelOrderButton.Dispose ();
                CancelOrderButton = null;
            }

            if (CurentOrderProductItemTable != null) {
                CurentOrderProductItemTable.Dispose ();
                CurentOrderProductItemTable = null;
            }

            if (DialogPanel != null) {
                DialogPanel.Dispose ();
                DialogPanel = null;
            }

            if (ListOrderItemTable != null) {
                ListOrderItemTable.Dispose ();
                ListOrderItemTable = null;
            }

            if (RecivedOrderNumberLabel != null) {
                RecivedOrderNumberLabel.Dispose ();
                RecivedOrderNumberLabel = null;
            }

            if (RecivedOrderTimeLabel != null) {
                RecivedOrderTimeLabel.Dispose ();
                RecivedOrderTimeLabel = null;
            }

            if (RecivedOrderTimerLabel != null) {
                RecivedOrderTimerLabel.Dispose ();
                RecivedOrderTimerLabel = null;
            }
        }
    }
}