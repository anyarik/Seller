using System;
using MvvmCross.Binding.iOS.Views;
using Foundation;
using UIKit;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using FoodPoint_Seller.Core.ViewModels;
using FoodPoint_Seller.Api.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables;

namespace FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables
{
    [Register(nameof(CustomerCell))]
    public partial class CustomerCell 
        : BaseViewCell<CustomerCell,CustomerDayInfo> 
    {
        public CustomerCell(IntPtr handle) 
            : base (handle)
        {
           
        }
    }
}