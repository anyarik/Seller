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

namespace Collections.Touch
{
    [Register ("OrderCell")]
    partial class OrderCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RowLabel { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UILabel Timerlbl { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIButton OverOrderBtn { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UITableView ProductTabel { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIKit.UIView TestView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (RowLabel != null) {
                RowLabel.Dispose ();
                RowLabel = null;
            }
            if (Timerlbl != null)
            {
                Timerlbl.Dispose();
                Timerlbl = null;
            }
            if (OverOrderBtn != null)
            {
                OverOrderBtn.Dispose();
                OverOrderBtn = null;
            }
            if (ProductTabel != null)
            {
                ProductTabel.Dispose();
                ProductTabel = null;
            }
            if (TestView != null)
            {
                TestView.Dispose();
                TestView = null;
            }
        }
    }
}