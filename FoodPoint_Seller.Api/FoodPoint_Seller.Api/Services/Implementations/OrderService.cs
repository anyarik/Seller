using FoodPoint_Seller.Api.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections;
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

        public async  void ChangeStatusOrder(string id, string state, bool isActive, TimeSpan time, string crashState, bool isOverPrevTimer)
        {
            var url = "";
            if (time == TimeSpan.Zero)
                url = $"{AppData.Host}/api/change_state_of_order?orderID={id}&newState={state}&isActive={isActive}";

            else
               url = $"{AppData.Host}/api/change_state_of_order?orderID={id}&newState={state}&isActive={isActive}&time={time}&crashState={crashState}&overPrevTimer={isOverPrevTimer}";

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

            var result =  await ConnectionService.PostStatusAsync(url, null, null,  "Не удалось получить профиль");

            //return result;
        }

        public async  void SetSellerOrder(string orderId, string sellerID)
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

            var result = await ConnectionService.PostStatusAsync(url, null, null, "Не удалось получить профиль");
        }
        public async Task<List<OrderItem>> GetActiveOrders(string sellerId, string token)
        {
            var url = $"{AppData.Host}/api/get_paidnactive_orders?sellerID={sellerId}";

            var orders = await ConnectionService.GetAsync<List<OrderItem>>(url, null, "Не удалось получить активные заказы");
            return orders;
        }

        public void SaveOfferedFood(string orderId, List<ProductForOrder> offeredProduct, string token)
        {
            string offeedProducts = getStringOfferedProducts(offeredProduct);

            var url = $"{AppData.Host}/api/no_food?{offeedProducts}";
                        
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

            var result = ConnectionService.PostStatusAsync(url, new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded"), null, "Не удалось получить профиль");
        }

        private static string getStringOfferedProducts(List<ProductForOrder> offeredProduct)
        {

            var offeedProducts = "";
            var indexProduct = 0;
            for (int i = 0; i < offeredProduct.Count; i++)
                if (!offeredProduct[i].ProductInfo.IsActive)
                    if (offeedProducts == "")
                    {
                        offeedProducts += $"wastedProducts[{indexProduct}]={offeredProduct[i].ID}";
                        indexProduct++;
                    }

                    else
                    {
                        offeedProducts += $"&wastedProducts[{indexProduct}]={offeredProduct[i].ID}";
                        indexProduct++;
                    }
                       


            var indexAdditive = 0;

            for (int i = 0; i < offeredProduct.Count; i++)
            {
                for (int j = 0; j < offeredProduct[i].ProductInfo.OrderedAdditives.Count; j++)
                {
                    if (!offeredProduct[i].ProductInfo.OrderedAdditives[j].IsActive)
                        if (offeedProducts == "")
                        {
                            offeedProducts += $"wastedAdditives[{indexAdditive}]={ offeredProduct[i].ProductInfo.OrderedAdditives[j].ID}";
                            indexAdditive++;
                        }
                        else
                        {
                            offeedProducts += $"&wastedAdditives[{indexAdditive}]={ offeredProduct[i].ProductInfo.OrderedAdditives[j].ID}";
                            indexAdditive++;
                        }
                }
            }
          
            return offeedProducts;
        }
    }
}
