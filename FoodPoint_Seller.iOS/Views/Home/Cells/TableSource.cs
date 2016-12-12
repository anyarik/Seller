
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

using CoreGraphics;
using FoodPoint_Seller.Touch.Views.Home;
using FoodPoint_Seller.Touch.Views.Home.Cells;
using MvvmCross.Binding.iOS.Views;

namespace FoodPoint_Seller.iOS
{
    public partial class TableSource : MvxSimpleTableViewSource
    {
        public TableSource(UITableView tableView)
       : base(tableView, "OrderCell", "OrderCell")
        {
        }
    }
}