using Cirrious.FluentLayouts.Touch;
using Foundation;
using UIKit;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.Binding.iOS.Views;
using System.Drawing;
using CoreGraphics;
using PickerCells;
using System.Collections.Generic;
using System;
using AllianceCustomPicker;
using Xam.DownPicker;

namespace FoodPoint_Seller.Touch.Views
{
    /// <summary>
    /// The login view
    /// </summary>
    [Register("LoginView")]
	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class LoginView : MvxViewController<LoginViewModel>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginView"/> class.
        /// </summary>
        public LoginView()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called after the controller’s <see cref="P:UIKit.UIViewController.View"/> is loaded into memory.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is called after <c>this</c> <see cref="T:UIKit.UIViewController"/>'s <see cref="P:UIKit.UIViewController.View"/> and its entire view hierarchy have been loaded into memory. This method is called whether the <see cref="T:UIKit.UIView"/> was loaded from a .xib file or programmatically.
        /// </para>
        /// </remarks>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(252, 80, 98);
            this.View.BackgroundColor =  UIColor.FromRGB(232, 232, 232); ;
      

            var scrollView = new UIScrollView(View.Frame)
            {
                ShowsHorizontalScrollIndicator = false,
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight
            };

                        
            var textEmail = new UITextField { Placeholder = "Username"
                                            , BorderStyle = UITextBorderStyle.RoundedRect };
            var textPassword = new UITextField { Placeholder = "Your password"
                                                , BorderStyle = UITextBorderStyle.RoundedRect
                                                , SecureTextEntry = true };

            var loginButton = new UIButton(UIButtonType.RoundedRect);
            loginButton.SetTitle("Войти", UIControlState.Normal);
            loginButton.BackgroundColor = UIColor.FromRGB(252, 80, 98); ;
            loginButton.SetTitleColor(UIColor.White, UIControlState.Normal);

            NSMutableArray array = new NSMutableArray() ;
            array.Add(new NSString("Продавец"));
            array.Add(new NSString("Управляющий"));

            //Add Data to our down picker outlet
            UIDownPicker picker = new UIDownPicker(array)
            {
                BorderStyle = UITextBorderStyle.RoundedRect
            }; 
            picker.Frame = this.View.Bounds;
            picker.DownPicker.SetToolbarDoneButtonText("Выбрать");
            picker.DownPicker.SetToolbarCancelButtonText("Закрыть");
            //picker.DownPicker.SetArrowImage(UIImage.);
            //picker.DownPicker.SetToolbarStyle(UIBarStyle.Default);
            picker.DownPicker.ShowArrowImage(true);
            picker.DownPicker.SetPlaceholderWhileSelecting("Выберете роль...");
            picker.DownPicker.SetPlaceholder("Выберете роль...");
            picker.EditingDidEnd += Picker_EditingDidEnd;

          var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(textEmail).To(vm => vm.Username); 
            set.Bind(textPassword).To(vm => vm.Password);
            set.Bind(loginButton).To("Login");
            set.Apply();

            Add(scrollView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                scrollView.AtLeftOf(View),
                scrollView.AtTopOf(View),
                scrollView.WithSameWidth(View),
                scrollView.WithSameHeight(View));

            scrollView.Add(picker);
            scrollView.Add(textEmail);
            scrollView.Add(textPassword);
            scrollView.Add(loginButton);

            scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var constraints = scrollView.VerticalStackPanelConstraints(new Margins(20, 10, 20, 10, 5, 5), scrollView.Subviews);
            scrollView.AddConstraints(constraints);
        }

        private void Picker_EditingDidEnd(object sender, EventArgs e)
        {
            ViewModel.CurrentRole.Value = ((UIDownPicker)sender).Text;
        }
        #endregion
    }
}