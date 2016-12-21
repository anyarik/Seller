using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services.Implementations
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

        public  void ChangeStatusOrder(string id, string state, bool isActive)
        {
            var url = $"{AppData.Host}/api/change_state_of_order?orderID={id}&newState={state}&isActive={isActive}";

            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            ////токен авторизации и другие headers.Add("")
            //headers.Add(
            //    new KeyValuePair<string, IEnumerable<string>>(
            //                                                    "Authorization",
            //                                                    new List<string>() {
            //                                                                        "Bearer "
            //                                                                        +token
            //                                                                        }
            //                                                    )
            //        );

            var result =  ConnectionService.PostStatus(url, null,  "Не удалось получить профиль");

            //return result;
        }

        public  void SetSellerOrder(string orderId, string sellerID)
        {
            var url = $"{AppData.Host}/api/set_seller?orderID={orderId}&sellerID={sellerID}";

            //List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            ////токен авторизации и другие headers.Add("")
            //headers.Add(
            //    new KeyValuePair<string, IEnumerable<string>>(
            //                                                    "Authorization",
            //                                                    new List<string>() {
            //                                                                        "Bearer "
            //                                                                        +token
            //                                                                        }
            //                                                    )
            //        );

            var result =  ConnectionService.PostStatus(url, null, "Не удалось получить профиль");
        }
    }
}
