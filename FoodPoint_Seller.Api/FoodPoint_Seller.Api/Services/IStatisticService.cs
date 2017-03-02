using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services
{
    public interface IStatisticService
    {
        Task<List<SellerDayInfo>> GetSellerStatisticForDay(string id, string beginDate, string endDate, string token);

        Task<List<FoodDayInfo>> GetFoodStatisticForDay(string id, string beginDate, string endDate, string token);
        Task<List<RevenueDayInfo>> GetRevenueStatisticForDay(string id, string beginDate, string endDate, string token);
        Task<List<AdditivesDayInfo>> GetAdditivesStatisticForDay(string id, string beginDate, string endDate, string token);
        Task<List<CustomerDayInfo>> GetCustomersStatisticForDay(string id, string beginDate, string endDate, string token);
        Task<List<OnlineSellerDayInfo>> GetOnlineSellersStatisticForDay(string id, string date, string token);

        Task<List<SellerAccountModel>> GetShopSellers(string establishmentId, string token);
    }
}
