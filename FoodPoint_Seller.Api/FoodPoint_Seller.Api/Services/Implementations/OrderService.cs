using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services
{
    public class OrderService : IOrderService
    {
        public void SendOrder(string order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppData.Host);

                var json = order;

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync($"/api/add_order", content);
          
            }
        }
    }
}
