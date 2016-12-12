namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class RestricitonsModel
    {
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public bool Aviability
        {
            get; set;
        }
    }
}