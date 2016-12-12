//using Cirrious.FluentLayouts.Touch;
//using Foundation;
//using MvvmCross.Binding.BindingContext;
//using UIKit;
//using FoodPoint_Seller.Core.ViewModels;
//using MvvmCross.iOS.Support.SidePanels;
//using MvvmCross.Binding.iOS.Views;
//using System.Collections.Generic;
//using MvvmCross.iOS.Views;

//namespace FoodPoint_Seller.Touch.Views
//{
//    [Register("HomeView")]
//	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
//    public class HomeView : BaseViewController<HomeViewModel>
//    {
//        public override void ViewDidLoad()
//        {
//            base.ViewDidLoad();
//            var viewModel = this.ViewModel;

//            var scrollView = new UIScrollView(View.Frame)
//            {
//                ShowsHorizontalScrollIndicator = false,
//                AutoresizingMask = UIViewAutoresizing.FlexibleHeight
//            };



//            //var source = new TableSource(TableView)
//            //{
//            //    UseAnimations = true,
//            //    AddAnimation = UITableViewRowAnimation.Left,
//            //    RemoveAnimation = UITableViewRowAnimation.Right
//            //};

//            //this.AddBindings(new Dictionary<object, string>
//            //    {
//            //        {source, "ItemsSource ListOrderItem"}
//            //    });

//            //TableView.Source = source;
//            //TableView.ReloadData();


//            Add(scrollView);

//            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

//            View.AddConstraints(
//                scrollView.AtLeftOf(View),
//                scrollView.AtTopOf(View),
//                scrollView.WithSameWidth(View),
//                scrollView.WithSameHeight(View));
//            scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

//            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
//            //set.Bind(TableView).To("ListOrderItem");
//            ////set.Apply();

//            //scrollView.AddConstraints(
//            //    TableView.Below(scrollView).Plus(10),
//            //    TableView.WithSameWidth(scrollView),
//            //    TableView.WithSameLeft(scrollView)
//            //    );

//        }

//        public override void ViewWillAppear(bool animated)
//        {
//            Title = "Home View";
//            base.ViewWillAppear(animated);
//        }
//    }

//    //public class TableSource : MvxSimpleTableViewSource
//    //{
//    //    public TableSource(UITableView tableView)
//    //        : base(tableView, "OrderCell", "OrderCell")
//    //    {
//    //    }
//    //}
//}
