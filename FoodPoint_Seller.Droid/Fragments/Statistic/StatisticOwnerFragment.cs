using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Shared.Attributes;
using FoodPoint_Seller.Core.ViewModels;
using CrossUI.Droid.Dialog.Elements;
using MvvmCross.Binding.BindingContext;

namespace FoodPoint_Seller.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("foodpoint_seller.droid.fragments.StatisticOwnerFragment")]
    public class StatisticOwnerFragment : BaseFragment<StatisticOwnerViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ShowHamburgerMenu = true;
            return base.OnCreateView(inflater, container, savedInstanceState);

            //new DateElement("The Date", DateTime.Today).Bind(this, "Value EndDateValue");
            //new DateElement("The Date", DateTime.Today).Bind(this, "Value StartDateValue");
        }

        protected override int FragmentId => Resource.Layout.fragment_owner_statistic;
    }
}
