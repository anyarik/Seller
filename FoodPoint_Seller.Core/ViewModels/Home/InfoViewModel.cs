using MvvmCross.Core.ViewModels;

namespace FoodPoint_Seller.Core.ViewModels
{
	public class InfoViewModel : BaseViewModel
    {
        public InfoViewModel()
        {
            Info = "This is info for you...";
        }

	    public string Info { get; private set; }

        private MvxCommand showThirdViewModelCommand;

        public MvxCommand ShowThirdViewModelCommand
        {
            get
            {
                showThirdViewModelCommand = showThirdViewModelCommand ?? new MvxCommand(DoShowThirdViewModel);
                return showThirdViewModelCommand;
            }
        }

        private void DoShowThirdViewModel()
        {
            ShowViewModel<ThirdViewModel>();
        }


    }
}