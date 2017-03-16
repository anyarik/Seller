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
//using MvvmCross.Droid.Shared.Attributes;
//using FoodPoint_Seller.Core.ViewModels;
//using CrossUI.Droid.Dialog.Elements;
//using MvvmCross.Binding.BindingContext;
//using Android.Support.V4.View;
//using MvvmCross.Droid.Support.V4;
//using Android.Support.Design.Widget;

//namespace FoodPoint_Seller.Droid.Fragments
//{
//    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
//    [Register("foodpoint_seller.droid.fragments.StatisticOwnerFragment")]
//    public class StatisticOwnerFragment : BaseFragment<StatisticOwnerViewModel>
//    {
//        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//        {
//            ShowHamburgerMenu = true;
//            var view = base.OnCreateView(inflater, container, savedInstanceState);

//            var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
//            if (viewPager != null)
//            {
//                var fragments = new List<MvxFragmentPagerAdapter.FragmentInfo>
//                {
//                    new MvxFragmentPagerAdapter.FragmentInfo("Выручка", typeof (OrdersStatisticFragment),
//                        typeof (OrdersStatisticViewModel)),
//                    new MvxFragmentPagerAdapter.FragmentInfo("Продукты", typeof (ProductStatisticFragment),
//                        typeof (ProductStatisticViewModel)),
//                    new MvxFragmentPagerAdapter.FragmentInfo("Продавцы", typeof (SellersStatisticFragment),
//                        typeof (SellersStatisticViewModel)),
//                    new MvxFragmentPagerAdapter.FragmentInfo("Работа продавцов", typeof (OnlineSellersStatisticFragment),
//                        typeof (OnlineSellersStatisticViewModel)),
//                    new MvxFragmentPagerAdapter.FragmentInfo("Клиенты", typeof (CustomersStatisticFragment),
//                        typeof (CustomersStatisticViewModel))
//                };
//                viewPager.Adapter = new MvxFragmentPagerAdapter(Activity, ChildFragmentManager, fragments);
//            }

//            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
//            tabLayout.SetupWithViewPager(viewPager);

//            return view;
//        }

//        protected override int FragmentId => Resource.Layout.fragment_owner_statistic;
//    }
//}
