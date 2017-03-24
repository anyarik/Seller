using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.Binding.BindingContext;


using MvvmCross.iOS.Support.SidePanels;
using Foundation;
using MvvmCross.iOS.Views;
using UIKit;
using CoreGraphics;
using FoodPoint_Seller.Touch.Views.Home;
using MvvmCross.Binding.iOS.Views;
using FoodPoint_Seller.Touch.Views.OrderDialog;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Platform;

namespace FoodPoint_Seller.Touch.Views
{
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public partial class HomeView : MvxViewController<HomeViewModel>
    {
        private UITableView _ordersTable;
        private UITableView _smallOrdersTable;

        private const float marginLeftTable = 60;
        private const float marginRightTable = 5;
        private const float widthOrdersTable = 0.7f;
        private const float widthSmallOrdersTable = 0.3f;


        private const float marginLeftOrderDialog = 10;
        private const float marginRightOrderDialog = 10;
        private const float marginTopOrderDialog = 10;
        private const float marginButtomOrderDialog = 10;

        private const float widthOrderDialog = 0.95f;
        private const float hightOrderDialog = 1f;
        private OrderDialogView _dialog;

        public HomeView() : base("HomeView", null)
        {
        }

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
            this.NavigationController.NavigationBar.Translucent = true;

            var toolbarButton = new UIBarButtonItem(""
                                                   , UIBarButtonItemStyle.Bordered
                                                   , (sender, args) =>
                                                   {
                                                       // button was clicked
                                                   })
            {
            };

            var toolbarStatus = new UIBarButtonItem( ""
                                                   , UIBarButtonItemStyle.Plain
                                                   , (sender, args) =>
                                                   {
                                                       // button was clicked
                                                   });

            this.NavigationItem.SetRightBarButtonItems(
                   new UIBarButtonItem[] { toolbarButton, toolbarStatus }
                  , true);

            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
            set.Bind(toolbarButton).To("OnClickOffline");
            set.Bind(toolbarButton).For(t=>t.Title).To(vm => vm.TextToolbarBtn);
            set.Bind(toolbarStatus).For(t => t.Title).To(vm => vm.TextStatusSeller);
            set.Apply();

            // this.CreateBinding(RecivedOrderTimeLabel).To((HomeViewModel vm) => vm.RecivedOrderTime).Apply();
            // this.CreateBinding(RecivedOrderTimerLabel).To((HomeViewModel vm) => vm.RecivedOrderTimer).Apply();

            // this.CreateBinding(CancelOrderButton).To("OnCancelOrder").Apply();
            // this.CreateBinding(CancelOrderButton).To("OnApprove").Apply();

            // this.CreateBinding(DialogPanel).For("Visibility")
            //.To<HomeViewModel>(vm => vm.IsOrderDialogOpen)
            //.WithConversion("Visibility").Apply();

            //this.CreateBinding(CurentOrderProductItemTable.Source).To((HomeViewModel vm) => vm.ListCurentOrderProductItem).Apply();
            //// Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this._ordersTable = new UITableView(
                new CGRect(0
                          , 0 + this.NavigationController.NavigationBar.Frame.Height
                          , View.Frame.Width * widthOrdersTable
                          , View.Frame.Height))
            {
                SeparatorStyle = UITableViewCellSeparatorStyle.None,
                BackgroundColor = UIColor.Clear,
                EstimatedRowHeight = 200,
                RowHeight = 200,
               
                //EstimatedSectionHeaderHeight = 30,
                SectionHeaderHeight = 30//UITableView.AutomaticDimension,
            };

            //_ordersTable.SectionHeaderHeight = 30;
            this._smallOrdersTable = new UITableView(
                new CGRect( _ordersTable.Frame.Width + marginLeftTable
                          , 0 + this.NavigationController.NavigationBar.Frame.Height
                          , View.Frame.Width * widthSmallOrdersTable - marginLeftTable - marginRightTable
                          , View.Frame.Height))
            {
                SeparatorStyle = UITableViewCellSeparatorStyle.None,
                BackgroundColor = UIColor.Clear,
                EstimatedRowHeight = 60,
                RowHeight = 60,
                
               // EstimatedSectionHeaderHeight = 30,
                SectionHeaderHeight = 30,
                
            };

            this._ordersTable.RegisterClassForCellReuse(typeof(OrderCell), OrderCell.Key);
            var sourceOrder = new MvxSimpleTableViewSource(this._ordersTable, OrderCell.Key
                                                                     , OrderCell.Key);
            this._ordersTable.Source = sourceOrder;
            
            this._smallOrdersTable.RegisterClassForCellReuse(typeof(SmallOrderCell), SmallOrderCell.Key);
            var sourceSmallOrder = new MvxSimpleTableViewSource(this._smallOrdersTable, SmallOrderCell.Key
                                                                     , SmallOrderCell.Key);
            this._smallOrdersTable.Source = sourceSmallOrder;
            
            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
            set.Bind(sourceSmallOrder).To(vm => vm.ListOrderItem);
            set.Bind(sourceOrder).To(vm => vm.ListOrderItem);
            set.Apply();

            this._smallOrdersTable.ReloadData();
            this._ordersTable.ReloadData();


            this._dialog = new OrderDialogView
            {
                View =
                {
                    Frame = new CGRect(0 + marginLeftOrderDialog
                        , this.NavigationController.NavigationBar.Frame.Height + marginTopOrderDialog
                        , View.Frame.Width * widthOrderDialog - marginRightOrderDialog
                        , View.Frame.Height * hightOrderDialog - marginButtomOrderDialog - marginTopOrderDialog),
                    Hidden = true
                }
            };
            //var setDialog = this.CreateBindingSet<HomeView, MainViewModel>();
            //setDialog.Bind(dialog).For("Visibility").To(vm => vm.IsOrderDialogOpen).WithConversion("Visibility");

            //setDialog.Apply();

            var top = Mvx.Resolve<IMvxIosView<MainViewModel>>();
            var act = top.ViewModel;

            this.CreateBinding(_dialog).For("Visibility").To<MainViewModel>(vm => vm.IsOrderDialogOpen)
                                                                                   .WithConversion("Visibility").Apply();
            
            this.View.AddSubview(this._dialog.View);
            this.View.AddSubview(this._ordersTable);
            this.View.AddSubview(this._smallOrdersTable);

            //_ordersTable.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            //var constraints = _ordersTable.VerticalStackPanelConstraints(new Margins(20, 10, 20, 10, 5, 5), _ordersTable.Subviews);
            //_ordersTable.AddConstraints(constraints);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            //float y = this.TopLayoutGuide.Length;
          //  this.dialog.ContentInset = new UIEdgeInsets(y, 0, 0, 0);
        }
    }
}