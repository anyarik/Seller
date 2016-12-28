using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Api.Services;

namespace FoodPoint_Seller.Api.Controllers.Implementations
{
   public  class StatisticControler : IStatisticController
    {
        IStatisticService _statisticService;

        public StatisticControler(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        public Task<List<SellerDayInfo>> GetSellerStatisticForDay(string id, string beginDate, string endDate) =>
                                                     this._statisticService.GetSellerStatisticForDay(id, beginDate, endDate);

        public Task<List<AdditivesDayInfo>> GetAdditivesStatisticForDay(string id, string beginDate, string endDate) =>
                                                     this._statisticService.GetAdditivesStatisticForDay(id, beginDate, endDate);

        public Task<List<FoodDayInfo>> GetFoodStatisticForDay(string id, string beginDate, string endDate) =>
                                                     this._statisticService.GetFoodStatisticForDay(id, beginDate, endDate);

        public Task<List<RevenueDayInfo>> GetRevenueStatisticForDay(string id, string beginDate, string endDate) =>
                                                     this._statisticService.GetRevenueStatisticForDay(id, beginDate, endDate);
    }
}
