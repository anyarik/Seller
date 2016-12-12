using System.Collections.Generic;

namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class ShopModel
    {
        public ShopModel()
        {
            //this.IsWorking = isWorkingNow(this.Schedule);
        }

        //private bool isWorkingNow(List<DayOfWork> schedule)
        //{
        //    bool isWorking = false;

        //    foreach (var day in schedule)
        //    {
        //        if (DateTime.UtcNow.ToString("dddd") != day.dayofweek)
        //            continue;

        //        var start = new TimeSpan();
        //        var end = new TimeSpan();

        //        TimeSpan.TryParse(day.starttime, out start);
        //        TimeSpan.TryParse(day.endtime, out end);

        //        isWorking = start < DateTime.UtcNow.TimeOfDay && DateTime.UtcNow.TimeOfDay < end;
        //    }

        //    return isWorking;
        //}

        public int ID { get; set; }
        public string Name { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public string Description { get; set; }
        public double XLocation { get; set; }
        public double YLocation { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
        public string Site { get; set; }
        public string Image { get; set; }
        public CompanyModel Company { get; set; }
        public List<SocNets> SocNets { get; set; }
        public List<DayOfWork> Schedule { get; set; }
        public List<ProductModel> ProductsSelling { get; set; }
        public List<RestricitonsModel> Restricitons { get; set; }
        //public bool IsWorking { get; set; }

    }
}
