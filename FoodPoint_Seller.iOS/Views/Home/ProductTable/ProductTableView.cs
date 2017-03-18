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
using FoodPoint_Seller.Api.Models.ViewModels;

namespace FoodPoint_Seller.Touch.Views.Home
{
    public class ProductTableView : MvxTableViewController
    {
        //    public ProductTableView(IMvxViewModel viewModel)
        //    {
        //        this.ViewModel = viewModel;
        //    }

        //    public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        //    {
        //        return 100;
        //    }

        //    public override void ViewDidLoad()
        //    {
        //        base.ViewDidLoad();

        //        var source = new ProductTableSource(TableView)
        //        {
        //            UseAnimations = false,
        //            AddAnimation = UITableViewRowAnimation.Left,
        //            RemoveAnimation = UITableViewRowAnimation.Right,


        //        };

        //        TableView.Source = source;

        //        this.AddBindings(new Dictionary<object, string>
        //            {
        //                {source, "ItemsSource ListOrderItem"}
        //            });

        //        TableView.ReloadData();

        //        TableView.Source = source;
        //        TableView.ReloadData();
        //    }

        //}
        public class ProductTableSource : MvxSimpleTableViewSource
        {
            private List<ProductForOrder> products;

            public ProductTableSource(UITableView tableView, List<ProductForOrder> products)
                : base(tableView, ProductCell.Key, ProductCell.Key)
            {
                this.products = products;
            }
            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return 1; 
            }
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(ProductCell.Key)
                       as ProductCell;
                //?? new ShopMainTableViewCell(cellIdentifier);

                if (cell == null)
                    cell = ProductCell.Create();

                //cell.Product = products[indexPath.Row];


                return cell/*base.GetCell(tableView, indexPath)*/;
            }
        }
    }
}