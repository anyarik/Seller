using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Extentions
{
    public static class GenericExtentions
    {
        public static INC<List<OrderItem>> AddItem(this INC<List<OrderItem>> list, OrderItem item)
        {
            var tempList = new List<OrderItem>();

            if (list.Value != null)
                tempList = list.Value;

            tempList.Add(item);

            list.Value = tempList ;
            return list;
        }
    }
}
