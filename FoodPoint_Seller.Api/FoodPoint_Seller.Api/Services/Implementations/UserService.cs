

using FoodPoint_Seller.Api.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services.Implementations
{
    public class UserService:IUserService
    {
        public async Task<AccessTokenAuthorise> AuthorizationSeller(SellerAccountModel user)
        {
            var url = $"{AppData.Identity}/connect/token";
            var data = $"username={user.EMail}&password={user.Password}&grant_type=password&scope=api";

            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers.Add(
            new KeyValuePair<string, IEnumerable<string>>(
                                                            "Authorization",
                                                            new List<string>() {
                                                                                    "Basic c2VsbGVyOnNlY3JldA=="
                                                                                }
                                                            )
                );
            var seller = await ConnectionService.PostAsync<AccessTokenAuthorise>(url
                                        , new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded")
                                        , headers, "Не удалось авторизоваться");

            return seller;
        }

        public async Task<AccessTokenAuthorise> AuthorizationOwner(OwnerAccountModel user)
        {
            var url = $"{AppData.Identity}/connect/token";
            var data = $"username={user.EMail}&password={user.Password}&grant_type=password&scope=api";

            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();
            headers.Add(
            new KeyValuePair<string, IEnumerable<string>>(
                                                            "Authorization",
                                                            new List<string>() {
                                                                                    "Basic Y2hpZWY6c2VjcmV0"
                                                                                }
                                                            )
                );
            var owner = await ConnectionService.PostAsync<AccessTokenAuthorise>(url
                                            , new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded")
                                            , headers, "Не удалось авторизоваться");

            return owner;
       }

        public async Task<OwnerAccountModel> GetProfileOwner(string id, string token)
        {
            var url = $"{AppData.Host}/api/get_chief_info?chiefID={id}";

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

            var profile = await ConnectionService.GetAsync<OwnerAccountModel>(url, headers, "Не удалось получить профиль");

            return profile;
        }

        public async Task<SellerAccountModel> GetProfileSeller(string id, string token)
        {
            var url = $"{AppData.Host}/api/get_seller_info?sellerID={id}";

            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

            headers.Add(
                new KeyValuePair<string, IEnumerable<string>>(
                                                                "Authorization",
                                                                new List<string>() {
                                                                                    $"Bearer {token}"
                                                                                    
                                                                                    }
                                                                )
                    );

            var profile = await ConnectionService.GetAsync<SellerAccountModel>(url, headers, "Не удалось получить профиль");

                return profile; 
        }

        public async Task<string> Set_Busyness(string id, bool busyness, string token)
        {
            var url = $"{AppData.Host}/api/set_busyness?sellerName={id}&busyness={busyness}";

            List<KeyValuePair<string, IEnumerable<string>>> headers = new List<KeyValuePair<string, IEnumerable<string>>>();

           
            //headers.Add(
            //    new KeyValuePair<string, IEnumerable<string>>(
            //                                                    "Authorization",
            //                                                    new List<string>() {
            //                                                                        "Bearer "
            //                                                                        +token
            //                                                                        }
            //                                                    )
            //        );

            var result = await ConnectionService.PostStatusAsync(url, null, headers, "Не послать запрос на изменение занятости");

            return result;
        }
    }
}
