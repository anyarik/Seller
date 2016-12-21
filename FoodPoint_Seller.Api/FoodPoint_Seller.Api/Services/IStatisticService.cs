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
        Task<List<SellerDayInfo>> GetSellerStatisticForDay(string id, string beginDate, string endDate);
    }
}
