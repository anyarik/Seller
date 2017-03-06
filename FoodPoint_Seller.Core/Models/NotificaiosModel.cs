using System;

namespace FoodPoint_Seller.Core.Models
{
    public class NotificaiosModel
    {
        public string title;
        public string description;

        public NotificaiosModel(string title, string description)
        {
            this.title = title;
            this.description = description;
        }
    }
}