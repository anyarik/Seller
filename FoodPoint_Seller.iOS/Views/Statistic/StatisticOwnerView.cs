using Foundation;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using XLPagerTabStrip;
using UIKit;
using CoreGraphics;
using System;
using MvvmCross.Platform;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("StatisticOwnerView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class StatisticOwnerView : MvxViewController<StatisticOwnerViewModel>
    {
        public override void ViewWillAppear(bool animated)
        {
            Title = "Statistic View";
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            NavigationController.SetNavigationBarHidden(true, true);
            base.ViewDidLoad();

            var navigationController = new UINavigationController();
            navigationController.NavigationBar.TitleTextAttributes =
                new UIStringAttributes() { ForegroundColor = UIColor.White };
            navigationController.NavigationBar.BarTintColor = UIColor.White;
            navigationController.NavigationBar.TintColor = UIColor.White;
            navigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            navigationController.NavigationBar.Translucent = false;
            navigationController.NavigationBar.ShadowImage = new UIImage();
            navigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);



            ViewController tabController = new ViewController();
            tabController.Title = "Awesome app";
            tabController.NavigationItem.Title = "Back";
            tabController.NavigationItem.BackBarButtonItem = new UIBarButtonItem("Back", UIBarButtonItemStyle.Plain, null);

            navigationController.PushViewController(tabController, false);

            ShowViewController(navigationController, null);
            //View.Window.RootViewController = navigationController;
            //View.Window.MakeKeyAndVisible();
            
            
            //View.AddSubview(navigationController);
            //RootViewController = navigationController;
            //View.AddSubview(ViewController);
        }
    }


    [Register("UniversalView")]
    public class UniversalView : UIView
    {
        public UniversalView()
        {
            Initialize();
        }

        public UniversalView(CGRect bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            BackgroundColor = UIColor.White;
        }
    }

    [Register("ViewController")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class ViewController : ButtonBarPagerTabStripViewController/*, IPagerTabStripDataSource*/
    {
        UIColor themeColor = UIColor.FromRGB(165, 16, 129);
        public ViewController()
        {

        }

        public ViewController(System.IntPtr handle)
            : base(handle)
        {
        }

        public ViewController(NSCoder coder) : base(coder)
        {

        }

        public ViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {

        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            var vm = Mvx.IocConstruct<HomeViewModel>();
            vm.ShowMenu();

            View = new UniversalView(View.Frame);
            View.BackgroundColor = UIColor.FromRGB(214, 214, 214);

            Settings.Style.ButtonBarBackgroundColor = themeColor;
            Settings.Style.ButtonBarItemBackgroundColor = themeColor;
            Settings.Style.SelectedBarBackgroundColor = UIColor.White;
            Settings.Style.ButtonBarItemFont = UIFont.BoldSystemFontOfSize(12);
            Settings.Style.SelectedBarHeight = 4;
            Settings.Style.ButtonBarMinimumLineSpacing = 0;
            Settings.Style.ButtonBarItemTitleColor = UIColor.White;
            Settings.Style.ButtonBarItemsShouldFillAvailiableWidth = true;
            Settings.Style.ButtonBarLeftContentInset = 0;
            Settings.Style.ButtonBarRightContentInset = 0;
            Settings.Style.ButtonBarHeight = 48;

            ChangeCurrentIndexProgressive = changeCurrentIndexProgressive;

            base.ViewDidLoad();

            // Perform any additional setup after loading the view
        }

        public override UIViewController[] CreateViewControllersForPagerTabStrip(PagerTabStripViewController pagerTabStripViewController)
        {
            OnlineSellersStatisticView onlineStatistic = new OnlineSellersStatisticView("Онлайн");
            OrdersStatisticView ordersStatistic = new OrdersStatisticView("Выручка");
            CustomersStatisticView customerStastic = new CustomersStatisticView("Клиенты");
            ProductStatisticView productStatistic = new ProductStatisticView("Товарная");
            SellersStatisticView sellerStatistic = new SellersStatisticView("Работники");

            return new UIViewController[] { onlineStatistic, ordersStatistic, sellerStatistic, customerStastic, productStatistic };
        }

        void changeCurrentIndexProgressive(ButtonBarViewCell oldCell, ButtonBarViewCell newCell, nfloat progressPercentage, bool changeCurrentIndex, bool animated)
        {
            //if (changeCurrentIndex == true)
            //{
            //    if (oldCell != null)
            //        oldCell.Label.TextColor = UIColor.White;
            //    if (newCell != null)
            //        newCell.Label.TextColor = crimson;
            //}
        }
    }
}
