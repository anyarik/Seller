using Foundation;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using System;
using MvvmCross.Platform;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using UIKit;
using FoodPoint_Seller.Core.Converters;
using FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("ProductStatisticView")]
	//[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class ProductStatisticView : MvxViewController<ProductStatisticViewModel>
    {

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            var a = this;
            this.ViewModel = Mvx.IocConstruct<ProductStatisticViewModel>();
            base.ViewDidLoad();

            //var textEmail = new UITextField { Placeholder = "Username", BorderStyle = UITextBorderStyle.RoundedRect };
            //var textPassword = new UITextField { Placeholder = "Your password", BorderStyle = UITextBorderStyle.RoundedRect, SecureTextEntry = true };
            var loginButton = new UIButton(new CGRect(100, 100, 100, 100));
            loginButton.SetTitle("фуд", UIControlState.Normal);
            loginButton.BackgroundColor = UIColor.Black;
            

            var set = this.CreateBindingSet<ProductStatisticView, ProductStatisticViewModel>();
            set.Bind(loginButton).For(l=>l.TitleLabel).To(vm => vm.StartDateValue);
            set.Bind(loginButton).To("SetStartTime");
            set.Apply();

            Add(loginButton);

            var table = new TemplateTableViewController<ProductCell
                                                   , ProductStatisticViewModel>
                            (ViewModel
                            , nameof(FoodStatisticValueConverter).Replace("ValueConverter", ""));

            View.AddSubview(table.View);

        }
    }
}
