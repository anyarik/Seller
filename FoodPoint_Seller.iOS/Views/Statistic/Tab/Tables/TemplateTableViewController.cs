using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables
{
    public class TemplateTableViewController<TCell, TViewModel>
        : MvxTableViewController
    {
        private string tableConverter;

        public TemplateTableViewController(IMvxViewModel viewModel, string tableConvertor)
        {
            this.ViewModel = viewModel;
            this.tableConverter = tableConvertor;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            var source = new TemplateTableSource<TCell>(TableView)
            {
                UseAnimations = false,
                AddAnimation = UITableViewRowAnimation.Left,
                RemoveAnimation = UITableViewRowAnimation.Right,
                
                
            };

            TableView.Source = source;

            var set = this.CreateBindingSet<TemplateTableViewController<TCell, TViewModel>, TViewModel>();
            set.Bind(source).To("StatisticListItem").WithConversion(this.tableConverter);
            set.Apply();

            TableView.ReloadData();
        }

    }
}
