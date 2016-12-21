using Foundation;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Views;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("StatisticView")]
	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class StatisticView : MvxViewController<StatisticSellerViewModel>
    {
        public override void ViewWillAppear(bool animated)
        {
            Title = "Statistic View";
            base.ViewWillAppear(animated);
        }
    }
}
