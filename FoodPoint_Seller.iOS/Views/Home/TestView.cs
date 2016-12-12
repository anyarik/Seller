using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using System;

using FoodPoint_Seller.Api.Controllers;

using UIKit;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.iOS;
using System.Collections.Generic;

using MvvmCross.iOS.Support.SidePanels;
using Foundation;

namespace FoodPoint_Seller.Touch.Views.Home
{
    
    //[Register("TestView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public partial class TestView : BaseViewController<HomeViewModel>
    {
        public override void ViewWillAppear(bool animated)
        {
            Title = "Home View";
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source= new TableSource(ListOrderItemTable);

            this.AddBindings(new Dictionary<object, string>
                {
                    {source, "ItemsSource ListOrderItem"}
                });

            ListOrderItemTable.Source = source;
            ListOrderItemTable.ReloadData();

            this.CreateBinding(CountCancelOrderLabel).To((HomeViewModel vm) => vm.CountCancelOrder).Apply();
            this.CreateBinding(CountDoneOrderLabel).To((HomeViewModel vm) => vm.CountDoneOrder).Apply();
            this.CreateBinding(CountNotAnswerOrderLabel).To((HomeViewModel vm) => vm.CountNotAnswerOrder).Apply();

            //this.CreateBinding(ListOrderItemTable.Source).To((HomeViewModel vm) => vm.ListOrderItem).Apply();
            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}