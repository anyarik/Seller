//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using FoodPoint_Seller.Core.ViewModels;
//using MvvmCross.Droid.Shared.Attributes;

//namespace FoodPoint_Seller.Droid.Fragments
//{
//    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
//    [Register("foodpoint_seller.droid.fragments.LoginOwnerFragment")]
//    public class LoginOwnerFragment : BaseFragment<LoginOwnerViewModel>
//    {
//        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//        {
//            ShowHamburgerMenu = true;
//            return base.OnCreateView(inflater, container, savedInstanceState);
//        }
//        public LoginOwnerFragment()
//        {

//        }
//        protected override int FragmentId => Resource.Layout.fragment_login_owner;
//    }
//}