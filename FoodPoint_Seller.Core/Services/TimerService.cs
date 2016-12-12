using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlatformMenus.Core.Services
{
   public class TimerService:ITimeService
    {
        private List<RecivedOrder> orderList = new List<RecivedOrder>();

         

        public void AddItemToList(RecivedOrder order) =>
                              this.orderList.Add(order);
        public void AddRangeItemToList(List<RecivedOrder> orderList) => this.orderList.AddRange(orderList);



        public List<RecivedOrder> GetAllItems() => this.orderList;
        public RecivedOrder GetCurrentItem(Guid id) => this.orderList.Find(item => item.Order.ID == id);
        public void RemoveItemFromList(Guid id) => this.orderList.RemoveAll(item => item.Order.ID == id);

        public TimerService()
        {
            
        }

    }
}
