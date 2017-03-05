using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.Binding.BindingContext;


using MvvmCross.iOS.Support.SidePanels;
using Foundation;
using MvvmCross.iOS.Views;

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
            //var viewModel = this.ViewModel;
            ////var source= new TableSource(ListOrderItemTable);

            ////this.AddBindings(new Dictionary<object, string>
            ////    {
            ////        {source, "ItemsSource ListOrderItem"}
            ////    });

            ////ListOrderItemTable.Source = source;
            ////ListOrderItemTable.ReloadData();



            // this.CreateBinding(RecivedOrderNumberLabel).To((HomeViewModel vm) => vm.RecivedOrderNumber).Apply();
            // this.CreateBinding(RecivedOrderTimeLabel).To((HomeViewModel vm) => vm.RecivedOrderTime).Apply();
            // this.CreateBinding(RecivedOrderTimerLabel).To((HomeViewModel vm) => vm.RecivedOrderTimer).Apply();

            // this.CreateBinding(CancelOrderButton).To("OnCancelOrder").Apply();
            // this.CreateBinding(CancelOrderButton).To("OnApprove").Apply();

            // this.CreateBinding(DialogPanel).For("Visibility")
            //.To<HomeViewModel>(vm => vm.IsOrderDialogOpen)
            //.WithConversion("Visibility").Apply();


            this.CreateBinding(ListOrderItemTable.Source).To((HomeViewModel vm) => vm.ListOrderItem).Apply();
            //this.CreateBinding(CurentOrderProductItemTable.Source).To((HomeViewModel vm) => vm.ListCurentOrderProductItem).Apply();
            //// Perform any additional setup after loading the view, typically from a nib.
        }
    }
}