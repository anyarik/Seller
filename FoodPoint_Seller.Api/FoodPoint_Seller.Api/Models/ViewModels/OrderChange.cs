using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class OrderChange
    {

        public OrderChange(string seller, bool confirmed, List<ProductForOrder> changedOrder, string changedTime)
        {
            this.seller = seller;
            this.confirmed = confirmed;
            this.changedOrder = changedOrder;
            this.changedTime = changedTime;
        }

        public string seller { get; set; }
        public bool confirmed { get; set; }
        public List<ProductForOrder> changedOrder { get; set; }
        public string changedTime { get; set; }
    }
}
