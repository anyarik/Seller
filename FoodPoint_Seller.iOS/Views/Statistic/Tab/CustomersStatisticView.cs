using Foundation;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using FoodPoint_Seller.Core.ViewModels;
using XLPagerTabStrip;
using System;
using UIKit;
using MvvmCross.Platform;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;
using MvvmCross.Binding.iOS.Views;
using System.Collections.Generic;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("CustomersStatisticView")]
    //[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class CustomersStatisticView : MvxViewController<CustomersStatisticViewModel>, IIndicatorInfoProvider
    {
        public string Title { get; set; }
        public CustomersStatisticView(IntPtr handle) : base(handle) { }
        public CustomersStatisticView(string title)
        {
            Title = title;
        }
        public IndicatorInfo IndicatorInfoForPagerTabStrip(PagerTabStripViewController pagerTabStripController)
        {
            return new IndicatorInfo(Title);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            var a = this;
            this.ViewModel = Mvx.IocConstruct<CustomersStatisticViewModel>();
            base.ViewDidLoad();



            var loginButton = new UIButton(new CGRect(100, 100, 100, 100));
            loginButton.SetTitle("Кастомер", UIControlState.Normal);
            loginButton.BackgroundColor = UIColor.Black;

            var startDate = new UILabel(new CGRect(100, 200, 100, 100));
            startDate.BackgroundColor = UIColor.Black;


            var set = this.CreateBindingSet<CustomersStatisticView, CustomersStatisticViewModel>();

            set.Bind(startDate).To(vm => vm.StartDateValue);
           // set.Bind(loginButton).For(l => l.TitleLabel.Text).To(vm => vm.StartDateValue);
            set.Bind(loginButton).To("SetStartTime");

            set.Apply();

            CustomerStatisticTableSource table = new CustomerStatisticTableSource();
            

            Add(loginButton);
            View.AddSubview(table.View);

        }
    }


    public class CustomerStatisticTableSource
       : MvxTableViewController
    {
        public override void ViewDidLoad()
        {
            ViewModel = Mvx.IocConstruct<CustomersStatisticViewModel>();
            base.ViewDidLoad();
            

            var source = new TableSource(TableView)
            {
                UseAnimations = true,
                AddAnimation = UITableViewRowAnimation.Left,
                RemoveAnimation = UITableViewRowAnimation.Right
            };

            this.AddBindings(new Dictionary<object, string>
                {
                    {source, "ItemsSource StatisticListItem"}
                });

            TableView.Source = source;
            TableView.ReloadData();
        }

        public class TableSource : MvxSimpleTableViewSource
        {
            public TableSource(UITableView tableView)
                : base(tableView, "CustomerCell", "CustomerCell")
            {
            }
        }
    }
  
}
