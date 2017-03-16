using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Android.Support.V4.View;
using MvvmCross.Droid.Support.V4;
using FoodPoint_Seller.Core.ViewModels;
using FoodPoint_Seller.Droid.Fragments;
using Android.Support.Design.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace FoodPoint_Seller.Droid.Activities
{
    [Activity(
        Label = "",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop,
        ScreenOrientation = ScreenOrientation.Landscape
        //Name = "foodPoint_Seller.droid.activities.StatisticOwnerActivity"
    )]
    public class StatisticOwnerActivity :  MvxCachingFragmentCompatActivity<StatisticOwnerViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_owner_statistic);

            var viewPager = this.FindViewById<ViewPager>(Resource.Id.viewpager);
            if (viewPager != null)
            {
                var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>
                {
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("Выручка", typeof (OrdersStatisticFragment),
                        typeof (OrdersStatisticViewModel)),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("Продукты", typeof (ProductStatisticFragment),
                        typeof (ProductStatisticViewModel)),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("Продавцы", typeof (SellersStatisticFragment),
                        typeof (SellersStatisticViewModel)),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("Работа продавцов", typeof (OnlineSellersStatisticFragment),
                        typeof (OnlineSellersStatisticViewModel)),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo("Клиенты", typeof (CustomersStatisticFragment),
                        typeof (CustomersStatisticViewModel))
                };
                viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(this.BaseContext, SupportFragmentManager, fragments);

                var tabLayout = this.FindViewById<TabLayout>(Resource.Id.tabs);

                tabLayout.SetupWithViewPager(viewPager);
                //viewPager.SetCurrentItem(0, true);
            }
        }
    }
}