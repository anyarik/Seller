

using FoodPoint_Seller.Api.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services
{
    public class UserService:IUserService
    {
        public async Task<AccessTokenAuthorise> Authorization(UserModel user)
        {
            var url = $"{AppData.Identity}/connect/token";
            var data = $"username={user.Email}&password={user.Password}&grant_type=password&scope=api";

            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers.Add(
            new KeyValuePair<string, IEnumerable<string>>(
                                                            "Authorization",
                                                            new List<string>() {
                                                                                    "Basic bW9iaWxlOnNlY3JldA=="
                                                                                }
                                                            )
                );

            return await ConnectionService.PostAsync<AccessTokenAuthorise>(url, new StringContent(data, Encoding.UTF8,  "application/x-www-form-urlencoded"), headers, "Не удалось авторизоваться");
        }

        public async Task<UserModel> GetProfileUser(int id, string token)
        {
            var url = $"{AppData.Host}/api/getProfileUser?userID={id}";

            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            //токен авторизации и другие headers.Add("")
            headers.Add(
                new KeyValuePair<string, IEnumerable<string>>(
                                                                "Authorization",
                                                                new List<string>() {
                                                                                    "Bearer "
                                                                                    +token
                                                                                    }
                                                                )
                    );

            var result = await ConnectionService.GetAsync<UserModel>(url, headers, "Не удалось получить профиль");

            return result; 
        }
    }
}
