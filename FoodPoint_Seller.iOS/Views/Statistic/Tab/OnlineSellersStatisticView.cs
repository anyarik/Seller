﻿using Foundation;
using MvvmCross.iOS.Views;
using FoodPoint_Seller.Core.ViewModels;
using UIKit;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;
using FoodPoint_Seller.Core.Converters;
using FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("OnlineSellersStatisticView")]
	//[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class OnlineSellersStatisticView : MvxViewController<OnlineSellersStatisticViewModel>//, IIndicatorInfoProvider
    {
        public override void ViewDidLoad()
        {
            //var a = this;
            //this.ViewModel = Mvx.IocConstruct<OnlineSellersStatisticViewModel>();
            base.ViewDidLoad();

            //var textEmail = new UITextField { Placeholder = "Username", BorderStyle = UITextBorderStyle.RoundedRect };
            //var textPassword = new UITextField { Placeholder = "Your password", BorderStyle = UITextBorderStyle.RoundedRect, SecureTextEntry = true };
            var loginButton = new UIButton(new CGRect(100,100,100,100));
            loginButton.SetTitle("Log in", UIControlState.Normal);
            loginButton.BackgroundColor = UIColor.Black;


            var set = this.CreateBindingSet<OnlineSellersStatisticView, OnlineSellersStatisticViewModel>();
            //set.Bind(loginButton).For(l=>l.TitleLabel).To(vm => vm.StartDateValue);
            loginButton.SetTitle("онлайн", UIControlState.Normal);
            set.Bind(loginButton).To("SetStartTime");
            set.Apply();

            Add(loginButton);

            var table = new TemplateTableViewController<OnlineSellerCell
                                       , OnlineSellersStatisticView>
                (ViewModel
                , nameof(OnlineSellerStatisticValueConverter).Replace("ValueConverter", ""));


            View.AddSubview(table.View);

        }
    }
}
