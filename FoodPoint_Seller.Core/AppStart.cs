using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.Services.Implementations;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace FoodPoint_Seller.Core
{
    /// <summary>
    /// A class that implements the IMvxAppStart interface and can be used to customise
    /// the way an application is initialised
    /// </summary>
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        /// <summary>
        /// The login service.
        /// </summary>
        private readonly ISellerAuthService _loginService;

        public AppStart(ISellerAuthService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Start is called on startup of the app
        /// Hint contains information in case the app is started with extra parameters
        /// </summary>
        public async void Start(object hint = null)
        {
            // If your application uses a secure API this first call attempts to log the user into the application
            // using any credentials stored from a previous session.  If there are
            // none stored we should present the login screen, else go straight into the app
             if (await _loginService.Login())
                ShowViewModel<MainViewModel>();

            else
                ShowViewModel<LoginViewModel>();

        }
    }
}