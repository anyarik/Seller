using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Api.Models.ViewModels.StatisticModel;

namespace FoodPoint_Seller.Api.Services.Implementations
{
    public class StatisticService : IStatisticService
    {


        ////токен авторизации и другие headers.Add("")

        //List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

        //headers.Add(
        //    new KeyValuePair<string, IEnumerable<string>>(
        //                                                    "Authorization",
        //                                                    new List<string>() {
        //                                                                        "Bearer "
        //                                                                        +token
        //                                                                        }
        //                                                    )
        //        );
        public async Task<List<SellerDayInfo>> GetSellerStatisticForDay(string sellerId, string beginDate, string endDate )
        {
            var url = $"{AppData.Host}/api/single_seller_statistics?sellerID={sellerId}&begin={beginDate}&end={endDate}";

            var result = await ConnectionService.PostAsync<List<SellerDayInfo>>(url, null, null, "Не удалось получить профиль");
            return result;
        }

        public async Task<List<FoodDayInfo>> GetFoodStatisticForDay(string establishmentId, string beginDate, string endDate)
        {
            
            var url = $"{AppData.Host}/api/food_statistics?sellerID={establishmentId}&begin={beginDate}&end={endDate}";

            var result = await ConnectionService.PostAsync<List<FoodDayInfo>>(url, null, null, "Не удалось получить профиль");
            return result;
        }
    }
}
