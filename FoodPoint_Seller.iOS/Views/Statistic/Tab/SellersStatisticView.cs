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

namespace FoodPoint_Seller.Touch.Views
{
    [Register("SellersStatisticView")]
	//[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class SellersStatisticView : MvxViewController<SellersStatisticViewModel>, IIndicatorInfoProvider
    {
        public string Title { get; set; }
        public SellersStatisticView(IntPtr handle) : base(handle) { }
        public SellersStatisticView(string title)
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
            this.ViewModel = Mvx.IocConstruct<SellersStatisticViewModel>();
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

        }
    }
}
