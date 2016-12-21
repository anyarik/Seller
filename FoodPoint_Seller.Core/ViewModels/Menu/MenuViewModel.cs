using MvvmCross.Core.ViewModels;

namespace FoodPoint_Seller.Core.ViewModels
{
	public class MenuViewModel : BaseViewModel
    {
        #region Cross Platform Commands & Handlers

        public IMvxCommand ShowHomeCommand
        {
            get { return new MvxCommand(ShowHomeExecuted); }
        }

        private void ShowHomeExecuted()
        {
            ShowViewModel<HomeViewModel>();
        }

        public IMvxCommand ShowStatisticSellerCommand
        {
            get { return new MvxCommand(ShowStatisticSellerExecuted); }
        }

        private void ShowStatisticSellerExecuted()
        {
            ShowViewModel<StatisticSellerViewModel>();
        }

        public IMvxCommand ShowHelpCommand
        {
            get { return new MvxCommand(ShowHelpExecuted); }
        }

        private void ShowHelpExecuted()
        {
            ShowViewModel<HelpAndFeedbackViewModel>();
        }

        #endregion

        #region Android Specific Demos

        public IMvxCommand ShowRecyclerCommand
        {
            get { return new MvxCommand(ShowRecyclerExecuted); }
        }

        private void ShowRecyclerExecuted()
        {
            ShowViewModel<ExampleRecyclerViewModel>();
        }

        public IMvxCommand ShowStatisticOwnerCommand
        {
            get { return new MvxCommand(ShowStatisticOwnerExecuted); }
        }

        private void ShowStatisticOwnerExecuted()
        {
            try
            {
                ShowViewModel<LoginOwnerViewModel>();
            }
            catch (System.Exception exp)
            {

                throw;
            }
        }

        #endregion
    }
}