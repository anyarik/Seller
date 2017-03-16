using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Foundation;
using System.Collections;
using FoodPoint_Seller.Touch.Views.Home.ProductTables.ProductCell;

namespace FoodPoint_Seller.Touch.Views.Home
{
    public class ProductTableSource : MvxSimpleTableViewSource
    {
        public ProductTableSource(UITableView tableView)
            : base(tableView, "ProductCell", "ProductCellInd")
        {

        }
    }
}