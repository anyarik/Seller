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
using MvvmCross.Binding.Droid.Target;
using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace FoodPoint_Seller.Droid.MvxBinding
{
    class LayoutHeightBinding : MvxAndroidTargetBinding
    {
        public LayoutHeightBinding(View target) : base(target)
        {

        }

        public override Type TargetType
        {
            get
            {
                return typeof(int);
            }
        }

        protected override void SetValueImpl(object target, object value)
        {
            var targetView = target as View;
            var valueRotate = value as List<AdditiveForProduct>;

            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            

            if(valueRotate != null)
                targetView.LayoutParameters.Height = ((int)(valueRotate.Count * act.Resources.DisplayMetrics.Density * 25));


        }
    }
}