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

namespace FoodPoint_Seller.Touch.Views
{
    [Register("OnlineSellersStatisticView")]
	//[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class OnlineSellersStatisticView : MvxViewController<OnlineSellersStatisticViewModel>, IIndicatorInfoProvider
    {
        public string Title { get; set; }
        public OnlineSellersStatisticView(IntPtr handle) : base(handle) { }
        public OnlineSellersStatisticView(string title)
        {
            Title = title;
        }
        public IndicatorInfo IndicatorInfoForPagerTabStrip(PagerTabStripViewController pagerTabStripController)
        {
            return new IndicatorInfo(Title);
        }

        public override void ViewWillAppear(bool animated)
        {
            //View.BackgroundColor = UIColor.Green; ;

            
            //var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            //set.Bind(textEmail).To(vm => vm.Username);
            //set.Bind(textPassword).To(vm => vm.Password);
            //set.Bind(loginButton).To("Login");
            //set.Apply();

            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            var a = this;
            this.ViewModel = Mvx.IocConstruct<OnlineSellersStatisticViewModel>();
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

        }
    }
}
