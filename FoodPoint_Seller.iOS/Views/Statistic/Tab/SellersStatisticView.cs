using Foundation;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using System;
using XLPagerTabStrip;
using MvvmCross.Platform;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using UIKit;
using FoodPoint_Seller.Core.Converters;
using FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("SellersStatisticView")]
	//[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class SellersStatisticView : MvxViewController<SellersStatisticViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //var textEmail = new UITextField { Placeholder = "Username", BorderStyle = UITextBorderStyle.RoundedRect };
            //var textPassword = new UITextField { Placeholder = "Your password", BorderStyle = UITextBorderStyle.RoundedRect, SecureTextEntry = true };
            var loginButton = new UIButton(new CGRect(100, 100, 100, 100));
            loginButton.SetTitle("Log in", UIControlState.Normal);
            loginButton.BackgroundColor = UIColor.Black;


            var set = this.CreateBindingSet<SellersStatisticView, SellersStatisticViewModel>();
            //set.Bind(loginButton).For(l=>l.TitleLabel).To(vm => vm.StartDateValue);
            loginButton.SetTitle("селер", UIControlState.Normal);
            set.Bind(loginButton).To("SetStartTime");
            set.Apply();

            Add(loginButton);

            var table = new TemplateTableViewController<SellerCell
                                       , SellersStatisticViewModel>
                (ViewModel
                , nameof(SellerStatisticValueConverter).Replace("ValueConverter", ""));

            View.AddSubview(table.View);


        }
    }
}
