//using Cirrious.FluentLayouts.Touch;
//using Foundation;
//using UIKit;
//using FoodPoint_Seller.Core.ViewModels;
//using MvvmCross.iOS.Views;
//using MvvmCross.Binding.BindingContext;
//using MvvmCross.iOS.Support.SidePanels;

//namespace FoodPoint_Seller.Touch.Views
//{
//    /// <summary>
//    /// The login view
//    /// </summary>
//    [Register("LoginOwnerView")]
//	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
//    public class LoginOwnerView : MvxViewController<LoginOwnerViewModel>
//    {
//        #region Constructors

//        /// <summary>
//        /// Initializes a new instance of the <see cref="LoginView"/> class.
//        /// </summary>
//        public LoginOwnerView()
//        {
//        }

//        #endregion

//        #region Public Methods

//        /// <summary>
//        /// Called after the controllerís <see cref="P:UIKit.UIViewController.View"/> is loaded into memory.
//        /// </summary>
//        /// <remarks>
//        /// <para>
//        /// This method is called after <c>this</c>†<see cref="T:UIKit.UIViewController"/>'s <see cref="P:UIKit.UIViewController.View"/> and its entire view hierarchy have been loaded into memory. This method is called whether the <see cref="T:UIKit.UIView"/> was loaded from a .xib file or programmatically.
//        /// </para>
//        /// </remarks>
//        public override void ViewDidLoad()
//        {
//            base.ViewDidLoad();
//            var viewModel = this.ViewModel;

//            var scrollView = new UIScrollView(View.Frame)
//            {
//                ShowsHorizontalScrollIndicator = false,
//                AutoresizingMask = UIViewAutoresizing.FlexibleHeight
//            };

//            var textEmail = new UITextField { Placeholder = "Username", BorderStyle = UITextBorderStyle.RoundedRect };
//            var textPassword = new UITextField { Placeholder = "Your password", BorderStyle = UITextBorderStyle.RoundedRect, SecureTextEntry = true };
//            var loginButton = new UIButton(UIButtonType.RoundedRect);
//            loginButton.SetTitle("Log in", UIControlState.Normal);
//            loginButton.BackgroundColor = UIColor.White;

//            var set = this.CreateBindingSet<LoginOwnerView, LoginOwnerViewModel>();
//            set.Bind(textEmail).To(vm => vm.Username);
//            set.Bind(textPassword).To(vm => vm.Password);
//            set.Bind(loginButton).To("Login");
//            set.Apply();

//            Add(scrollView);

//            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

//            View.AddConstraints(
//                scrollView.AtLeftOf(View),
//                scrollView.AtTopOf(View),
//                scrollView.WithSameWidth(View),
//                scrollView.WithSameHeight(View));

//            scrollView.Add(textEmail);
//            scrollView.Add(textPassword);
//            scrollView.Add(loginButton);

//            scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

//            var constraints = scrollView.VerticalStackPanelConstraints(new Margins(20, 10, 20, 10, 5, 5), scrollView.Subviews);
//            scrollView.AddConstraints(constraints);
//        }

//        #endregion
//    }
//}