using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Models;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Extentions
{
    public static class GenericExtentions
    {
        public static ObservableCollection<PayedOrder> RemoveItem(this ObservableCollection<PayedOrder> list, PayedOrder deleteItem)
        {
            foreach (var item in list)
            {
                if (item.Order.ID == deleteItem.Order.ID)
                {
                    list.Remove(item);
                }
            }
            return list;
        }
    }
}
