using Foundation;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;
using UIKit;
using CoreGraphics;
using System;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("StatisticOwnerView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class StatisticOwnerView : MvxTabBarViewController<StatisticOwnerViewModel>
    {
        public override void ViewWillAppear(bool animated)
        {
            Title = "Statistic View";
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            //NavigationController.SetNavigationBarHidden(true, true);
            base.ViewDidLoad();

            var viewControllers = new UIViewController[]
                                {
                                    CreateTabFor("Онлайн", "Онлайн", Mvx.IocConstruct<OnlineSellersStatisticViewModel>()),
                                    CreateTabFor("Клиенты", "twitter", Mvx.IocConstruct<CustomersStatisticViewModel>()),
                                    CreateTabFor("Работники", "favorites", Mvx.IocConstruct<SellersStatisticViewModel>()),
                                    CreateTabFor("Выручка", "favorites", Mvx.IocConstruct<OrdersStatisticViewModel>()),
                                    CreateTabFor("Товарная", "favorites", Mvx.IocConstruct<ProductStatisticViewModel>()),

                                };
            ViewControllers = viewControllers;
            CustomizableViewControllers = new UIViewController[] { };
            SelectedViewController = ViewControllers[0];
        }

        private UIViewController CreateTabFor(string title, string imageName, IMvxViewModel viewModel)
        {
            var controller = new UINavigationController();
            var screen = this.CreateViewControllerFor(viewModel) as UIViewController;
            // SetTitleAndTabBarItem(screen, title, imageName);
            if (screen != null)
            {
                screen.Title = title;
                controller.PushViewController(screen, false);
            }
            return controller;
        }

        //private void SetTitleAndTabBarItem(UIViewController screen, string title, string imageName)
        //{
        //    screen.Title = title;
        //    screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle("Images/Tabs/" + imageName + ".png"),
        //                                         _createdSoFarCount);
        //    _createdSoFarCount++;
        //}
    }
}
