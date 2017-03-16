using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.Binding.BindingContext;


using MvvmCross.iOS.Support.SidePanels;
using Foundation;
using MvvmCross.iOS.Views;
using UIKit;
using CoreGraphics;
using FoodPoint_Seller.Touch.Views.Home;

namespace FoodPoint_Seller.Touch.Views
{
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public partial class HomeView : MvxViewController<HomeViewModel>
    {
        public HomeView() : base("HomeView", null)
        {
        }

        //public override void ViewWillAppear(bool animated)
        //{
        //    Title = "Home View";
        //    base.ViewWillAppear(animated);
        //}
   

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();


            // Clear any references to subviews of the main view in order to
            // allow the Garbage Collector to collect them sooner.
            //
            // e.g. myOutlet.Dispose (); myOutlet = null;

            ReleaseDesignerOutlets();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.ShowMenu();
            this.View.BackgroundColor = UIColor.FromRGB(232, 232, 232);
            this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(252, 80, 98);
            this.NavigationController.NavigationBar.TintColor = UIColor.White;

            var toolbarButton = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (sender, args) =>
            {
                // button was clicked
            });
            this.NavigationItem.SetRightBarButtonItem(
                   toolbarButton
                , true);

            this.CreateBinding(toolbarButton).To("OnClickOffline").Apply();

            // this.CreateBinding(RecivedOrderTimeLabel).To((HomeViewModel vm) => vm.RecivedOrderTime).Apply();
            // this.CreateBinding(RecivedOrderTimerLabel).To((HomeViewModel vm) => vm.RecivedOrderTimer).Apply();

            // this.CreateBinding(CancelOrderButton).To("OnCancelOrder").Apply();
            // this.CreateBinding(CancelOrderButton).To("OnApprove").Apply();

            // this.CreateBinding(DialogPanel).For("Visibility")
            //.To<HomeViewModel>(vm => vm.IsOrderDialogOpen)
            //.WithConversion("Visibility").Apply();

            //this.CreateBinding(OrdersList).For(o=>o.s).To((HomeViewModel vm) => vm.ListOrderItem).Apply();
            //this.CreateBinding(CurentOrderProductItemTable.Source).To((HomeViewModel vm) => vm.ListCurentOrderProductItem).Apply();
            //// Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var OrdersList = new OrderTableView(this.ViewModel);
            OrdersList.TableView.Frame = new CGRect(0, 50, 400, View.Frame.Height);
            //OrdersList.TableView.SeparatorColor = UIColor.FromRGBA(0, 0, 0, 0);
            ////   OrdersList.TableView.
            OrdersList.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            OrdersList.TableView.EstimatedRowHeight = 200;
            OrdersList.TableView.RowHeight = 200;
            OrdersList.TableView.ReloadData();

            OrdersList.TableView.LayoutMargins = new UIEdgeInsets(10, 10, 10, 10);

            this.View.AddSubview(OrdersList.View);
        }

    }
}