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
using FoodPoint_Seller.Core.ViewModels.OrderDialog;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;

namespace FoodPoint_Seller.Droid.Fragments.OrderDialog
{
    [MvxFragment(typeof(OrderDialogViewModel), Resource.Id.content_frame)]
    [Register("foodpoint_seller.droid.fragments.OrderDialogFragment")]
    public class OrderDialogFragment : BaseDialogFragment<OrderDialogViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_recived_order;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}