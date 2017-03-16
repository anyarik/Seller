namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class AccountModel
    {
        public string ID;

        public string Name;
        public string EMail;
        public int shopID;
        public string Password;

        public AccountModel(string email, string password)
        {
            this.EMail = email;
            this.Password = password;
        }
        public AccountModel()
        {

        }
    }
}