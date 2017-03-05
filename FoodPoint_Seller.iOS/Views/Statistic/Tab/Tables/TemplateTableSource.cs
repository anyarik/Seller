using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace FoodPoint_Seller.Touch.Views.Statistic.Tab
{
    public class TemplateTableSource<TCell> : MvxSimpleTableViewSource
    {
        public TemplateTableSource(UITableView tableView)
            : base(tableView, typeof(TCell), nameof(TCell))
        {
        }
    }
}