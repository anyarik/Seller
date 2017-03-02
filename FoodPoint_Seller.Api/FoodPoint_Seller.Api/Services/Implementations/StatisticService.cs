using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Api.Models.DomainModels;

namespace FoodPoint_Seller.Api.Services.Implementations
{
    public class StatisticService : IStatisticService
    {

        public async Task<List<SellerDayInfo>> GetSellerStatisticForDay(string sellerId, string beginDate, string endDate , string token)
        {
            var url = $"{AppData.Host}/api/single_seller_statistics?sellerID={sellerId}&begin={beginDate}&end={endDate}";
            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            headers.Add(
                new KeyValuePair<string, IEnumerable<string>>(
                                                                "Authorization",
                                                                new List<string>() {
                                                                                    $"Bearer {token}"

                                                                                    }
                                                                )
                    );
            var result = await ConnectionService.GetAsync<List<SellerDayInfo>>(url, headers, "Не удалось получить одного продовца");
            return result;
        }

        public async Task<List<FoodDayInfo>> GetFoodStatisticForDay(string establishmentId, string beginDate, string endDate, string token)
        {
            var url = $"{AppData.Host}/api/food_statistics?establishmentID={establishmentId}&begin={beginDate}&end={endDate}";
            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            headers.Add(
                new KeyValuePair<string, IEnumerable<string>>(
                                                                "Authorization",
                                                                new List<string>() {
                                                                                    $"Bearer {token}"

                                                                                    }
                                                                )
                    );

            var result = await ConnectionService.GetAsync<List<FoodDayInfo>>(url, headers, "Не удалось получить статистику продуктов");
            return result;
        }
        
        public async Task<List<RevenueDayInfo>> GetRevenueStatisticForDay(string establishmentId, string beginDate, string endDate, string token)
        {
            var url = $"{AppData.Host}/api/revenue_statistics?establishmentID={establishmentId}&begin={beginDate}&end={endDate}";

            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            headers.Add(
                new KeyValuePair<string, IEnumerable<string>>(
                                                                "Authorization",
                                                                new List<string>() {
                                                                                    $"Bearer {token}"

                                                                                    }
                                                                )
                    );
            var result = await ConnectionService.GetAsync<List<RevenueDayInfo>>(url, headers,  "Не удалось получить статистику выручки");
            return result;
        }

        public async Task<List<AdditivesDayInfo>> GetAdditivesStatisticForDay(string establishmentId, string beginDate, string endDate, string token)
        {
            var url = $"{AppData.Host}/api/additives_revenue?establishmentID={establishmentId}&begin={beginDate}&end={endDate}";
            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            headers.Add(
                new KeyValuePair<string, IEnumerable<string>>(
                                                                "Authorization",
                                                                new List<string>() {
                                                                                    $"Bearer {token}"

                                                                                    }
                                                                )
                    );
            var result = await ConnectionService.GetAsync<List<AdditivesDayInfo>>(url, headers,  "Не удалось получить статистику добавок");
            return result;
        }

        public async Task<List<CustomerDayInfo>> GetCustomersStatisticForDay(string establishmentId, string beginDate, string endDate, string token)
        {
            var url = $"{AppData.Host}/api/customer_statistics?establishmentID={establishmentId}&begin={beginDate}&end={endDate}";
            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            headers.Add(
                new KeyValuePair<string, IEnumerable<string>>(
                                                                "Authorization",
                                                                new List<string>() {
                                                                                    $"Bearer {token}"

                                                                                    }
                                                                )
                    );
            var result = await ConnectionService.GetAsync<List<CustomerDayInfo>>(url, headers, "Не удалось получить статистику клиентов");
            return result;
        }

        public async Task<List<OnlineSellerDayInfo>> GetOnlineSellersStatisticForDay(string sellerId, string date, string token)
        {
            var url = $"{AppData.Host}/api/get_attendance_info?sellerId={sellerId}&day={date}";
            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            //headers.Add(
            //    new KeyValuePair<string, IEnumerable<string>>(
            //                                                    "Authorization",
            //                                                    new List<string>() {
            //                                                                        $"Bearer {token}"

            //                                                                        }
            //                                                    )
            //        );
            var result = await ConnectionService.GetAsync<List<OnlineSellerDayInfo>>(url, headers, "Не удалось получить статистику клиентов");
            return result;
        }

        public async Task<List<SellerAccountModel>> GetShopSellers(string establishmentId,  string token)
        {
            var url = $"{AppData.Host}/api/get_sellers_info?estID={establishmentId}";
            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            headers.Add(
                new KeyValuePair<string, IEnumerable<string>>(
                                                                "Authorization",
                                                                new List<string>() {
                                                                                    $"Bearer {token}"

                                                                                    }
                                                                )
                    );
            var result = await ConnectionService.GetAsync<List<SellerAccountModel>>(url, headers, "Не удалось получить статистику клиентов");
            return result;
        }

    }
}
