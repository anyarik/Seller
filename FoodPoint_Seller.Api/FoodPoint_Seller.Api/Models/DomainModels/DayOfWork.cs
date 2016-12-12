using System;

namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class DayOfWork
    {
        public Guid ID { get; set; }
        public string dayofweek { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
    }
}
