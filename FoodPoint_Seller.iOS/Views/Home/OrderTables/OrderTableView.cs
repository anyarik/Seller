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


namespace FoodPoint_Seller.Touch.Views.Home
{
    public class OrderTableView : MvxTableViewController
    {
        public OrderTableView(IMvxViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 200;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new TableSource(TableView)
            {
                UseAnimations = false,
                AddAnimation = UITableViewRowAnimation.Left,
                RemoveAnimation = UITableViewRowAnimation.Right,
                
                
            };

            TableView.Source = source;

            this.AddBindings(new Dictionary<object, string>
                {
                    {source, "ItemsSource ListOrderItem"}
                });

            TableView.ReloadData();

            TableView.Source = source;
            TableView.ReloadData();
        }

        public class TableSource : MvxSimpleTableViewSource
        {
            public TableSource(UITableView tableView)
                : base(tableView, "OrderCell", "OrderCellInd")
            {
            }
        }
    }
}   