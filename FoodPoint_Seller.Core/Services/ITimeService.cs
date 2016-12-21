using FoodPoint_Seller.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services
{
    public interface ITimeService
    {
        void AddItemToList(RecivedOrder order);
        void AddRangeItemToList(List<RecivedOrder> orderList);
        void RemoveItemFromList(Guid id);
        RecivedOrder GetCurrentItem(Guid id);
        List<RecivedOrder> GetAllItems();
        //void ContinueOrderInvoke();

        //event EventHandler eventTimer;
        //void funk(Action funk);
    }
}
