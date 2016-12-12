using Foundation;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.iOS.Support.SidePanels;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("SettingsView")]
	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class SettingsView : BaseViewController<SettingsViewModel>
    {
        public override void ViewWillAppear(bool animated)
        {
            Title = "Settings View";
            base.ViewWillAppear(animated);
        }
    }
}
