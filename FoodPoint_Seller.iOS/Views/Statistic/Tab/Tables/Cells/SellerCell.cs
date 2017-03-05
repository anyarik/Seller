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
    [Register(nameof(SellerCell))]
    public partial class SellerCell
        : BaseViewCell<SellerCell, SellerDayInfo> 
    {
        public SellerCell(IntPtr handle) 
            : base (handle)
        {
           
        }
    }
}