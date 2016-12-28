using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPoint_Seller.Api.Models.ViewModels;


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

            var result = await ConnectionService.GetAsync<List<SellerDayInfo>>(url, null, "Не удалось получить оодного продовца");
            return result;
        }

        public async Task<List<FoodDayInfo>> GetFoodStatisticForDay(string establishmentId, string beginDate, string endDate)
        {
            var url = $"{AppData.Host}/api/food_statistics?establishmentID={establishmentId}&begin={beginDate}&end={endDate}";

            var result = await ConnectionService.GetAsync<List<FoodDayInfo>>(url, null, "Не удалось получить статистику продуктов");
            return result;
        }

        public async Task<List<RevenueDayInfo>> GetRevenueStatisticForDay(string establishmentId, string beginDate, string endDate)
        {
            var url = $"{AppData.Host}/api/revenue_statistics?establishmentID={establishmentId}&begin={beginDate}&end={endDate}";

            var result = await ConnectionService.GetAsync<List<RevenueDayInfo>>(url, null,  "Не удалось получить статистику выручки");
            return result;
        }

        public async Task<List<AdditivesDayInfo>> GetAdditivesStatisticForDay(string establishmentId, string beginDate, string endDate)
        {
            var url = $"{AppData.Host}/api/additives_revenue?establishmentID={establishmentId}&begin={beginDate}&end={endDate}";

            var result = await ConnectionService.GetAsync<List<AdditivesDayInfo>>(url, null,  "Не удалось получить статистику добавок");
            return result;
        }
    }
}
