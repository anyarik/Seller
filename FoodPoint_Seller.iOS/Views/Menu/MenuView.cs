using MvvmCross.Binding.BindingContext;
using CoreGraphics;
using Foundation;
using UIKit;
using FoodPoint_Seller.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.iOS.Support.SidePanels;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("MenuView")]
	[MvxPanelPresentation(MvxPanelEnum.Left, MvxPanelHintType.ActivePanel, false)]
    public class MenuView : BaseViewController<MenuViewModel>
    {
        public MenuView()
        {}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var scrollView = new UIScrollView(View.Frame)
            {
                ShowsHorizontalScrollIndicator = false,
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight
            };

            // create a binding set for the appropriate view model
            var set = this.CreateBindingSet<MenuView, MenuViewModel>();

            var homeButton = new UIButton(new CGRect(0, 100, 320, 40));
            homeButton.SetTitle("Home", UIControlState.Normal);
            homeButton.BackgroundColor = UIColor.White;
            homeButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            set.Bind(homeButton).To(vm => vm.ShowHomeCommand);

            var statisticSellerButton = new UIButton(new CGRect(0, 100, 320, 40));
            statisticSellerButton.SetTitle("Статистика для продавца", UIControlState.Normal);
            statisticSellerButton.BackgroundColor = UIColor.White;
            statisticSellerButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            set.Bind(statisticSellerButton).To(vm => vm.ShowStatisticSellerCommand);

            var statisticOwnerButton = new UIButton(new CGRect(0, 100, 320, 40));
            statisticOwnerButton.SetTitle("Статистика для владельца", UIControlState.Normal);
            statisticOwnerButton.BackgroundColor = UIColor.White;
            statisticOwnerButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            set.Bind(statisticOwnerButton).To(vm => vm.ShowStatisticOwnerCommand);

            var settingButton = new UIButton(new CGRect(0, 100, 320, 40));
            settingButton.SetTitle("Настройки", UIControlState.Normal);
            settingButton.BackgroundColor = UIColor.White;
            settingButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            set.Bind(settingButton).To(vm => vm.ShowHelpCommand);

            var exitButton = new UIButton(new CGRect(0, 100, 320, 40));
            exitButton.SetTitle("Выйти", UIControlState.Normal);
            exitButton.BackgroundColor = UIColor.White;
            exitButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            set.Bind(exitButton).To(vm => vm.ShowHelpCommand);

            set.Apply();

            Add(scrollView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                scrollView.AtLeftOf(View),
                scrollView.AtTopOf(View),
                scrollView.WithSameWidth(View),
                scrollView.WithSameHeight(View));

            scrollView.Add(homeButton);
            scrollView.Add(statisticSellerButton);
            scrollView.Add(statisticOwnerButton);
            scrollView.Add(settingButton);
            scrollView.Add(exitButton);

            scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var constraints = scrollView.VerticalStackPanelConstraints(new Margins(20, 10, 20, 10, 5, 5), scrollView.Subviews);
            scrollView.AddConstraints(constraints);
        }

        public override void ViewWillAppear(bool animated)
        {
            Title = "Left Menu View";
            base.ViewWillAppear(animated);

            NavigationController.NavigationBarHidden = true;
        }
    }
}
