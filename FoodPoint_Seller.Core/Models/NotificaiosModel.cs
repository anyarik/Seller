using System;

namespace FoodPoint_Seller.Core.Models
{
    public class NotificaiosModel
    {
        public TimeSpan orderTimer;
        public string rowNumber;

        public NotificaiosModel(string rowNumber, TimeSpan orderTimer)
        {
            this.rowNumber = rowNumber;
            this.orderTimer = orderTimer;
        }
    }
}