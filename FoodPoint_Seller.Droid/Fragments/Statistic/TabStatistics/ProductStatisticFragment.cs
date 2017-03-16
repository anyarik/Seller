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
using Android.Support.Design.Widget;
using MvvmCross.Droid.Support.V4;
using Android.Support.V4.View;
using FoodPoint_Seller.Droid.Activities;
using MvvmCross.Binding.Droid.BindingContext;

namespace FoodPoint_Seller.Droid.Fragments
{
    //[MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [MvxFragment(typeof(StatisticOwnerViewModel), Resource.Id.content_statistic_frame)]
    [Register("foodpoint_seller.droid.fragments.ProductStatisticFragment")]
    public class ProductStatisticFragment : MvxFragment<ProductStatisticViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = base.OnCreateView(inflater, container, savedInstanceState);
            view = this.BindingInflate(Resource.Layout.fragment_statistic_food, null);
            return view;
        }

       // protected override int FragmentId => Resource.Layout.fragment_statistic_food;
    }
}
