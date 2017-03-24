using Foundation;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using FoodPoint_Seller.Core.ViewModels;

using System;
using UIKit;
using MvvmCross.Platform;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;
using MvvmCross.Binding.iOS.Views;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

using FoodPoint_Seller.Core.Converters;
using FoodPoint_Seller.Touch.Views.Statistic.Tab;
using FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("CustomersStatisticView")]
    //[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class CustomersStatisticView : MvxViewController<CustomersStatisticViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var loginButton = new UIButton(new CGRect(100, 100, 100, 100));
            loginButton.SetTitle("Кастомер", UIControlState.Normal);
            loginButton.BackgroundColor = UIColor.Black;

            var startDate = new UILabel(new CGRect(100, 200, 100, 100))
            {
                BackgroundColor = UIColor.Black
            };

            var set = this.CreateBindingSet<CustomersStatisticView, CustomersStatisticViewModel>();

            set.Bind(startDate).To(vm => vm.StartDateValue);
           // set.Bind(loginButton).For(l => l.TitleLabel.Text).To(vm => vm.StartDateValue);
            set.Bind(loginButton).To("SetStartTime");

            set.Apply();

            var table = new TemplateTableViewController<CustomerCell, CustomersStatisticViewModel>
            (ViewModel
                , nameof(CustomerStatisticValueConverter).Replace("ValueConverter", ""))
            {
                View = {Frame = new CGRect(0, 50, View.Frame.Width, 300)}
            };


            Add(loginButton);
            View.AddSubview(table.View);
        }
    }
}
