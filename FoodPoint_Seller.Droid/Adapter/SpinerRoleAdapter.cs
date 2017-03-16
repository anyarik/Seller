using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Binding.ExtensionMethods;

namespace FoodPoint_Seller.Droid.Adapter
{
    public class SpinerRoleAdapter : MvxAdapter
    {
        public SpinerRoleAdapter(Context context) : base(context)
        {
        }

        public override int Count => this.ItemsSource.Count() - 1;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            
            return base.GetView(position, convertView, parent);
        }
        public SpinerRoleAdapter(Context context, IMvxAndroidBindingContext bindingContext) : base(context, bindingContext)
        {
        }

        protected SpinerRoleAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}