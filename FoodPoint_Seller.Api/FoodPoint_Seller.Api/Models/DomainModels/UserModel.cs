namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class UserModel
    {
        public UserModel(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public UserModel(string email, string password, string login) : this(email, password)
        {
            this.Name = login;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
