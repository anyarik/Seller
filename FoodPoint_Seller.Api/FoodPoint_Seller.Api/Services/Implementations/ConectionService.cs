using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FoodPoint_Seller.Api.Services.Implementations
{
    internal static class ConnectionService
    {
        #region Async
        public static async Task<T> GetAsync<T>(string url, List<KeyValuePair<string
                        , IEnumerable<string>>> headers = null, string errorMessage = null)
        {
            return await ProcessRequestAsync<T>(url, null, headers, errorMessage);
        }

        public static async Task<T> PostAsync<T>(string url, HttpContent postData
                        , List<KeyValuePair<string, IEnumerable<string>>> headers = null, string errorMessage = null)
        {
            return await ProcessRequestAsync<T>(url, postData, headers, errorMessage);
        }



        public static async Task<string> PostStatusAsync(string url, HttpContent postData
                        , List<KeyValuePair<string, IEnumerable<string>>> headers = null, string errorMessage = null)
        {
            return await ProcessRequestStatusAsync(url, postData, headers, errorMessage);
        }

        private static async Task<T> ProcessRequestAsync<T>(string url, HttpContent postData
                         , List<KeyValuePair<string, IEnumerable<string>>> headers, string errorMessage)
        {
            using (var handler = new HttpClientHandler())
            {
                //if (handler.SupportsAutomaticDecompression)
                //    handler.AutomaticDecompression = DecompressionMethods.Deflate;

                using (var httpClient = new HttpClient(handler))
                {
                    using (var message = new HttpRequestMessage())
                    {
                        message.RequestUri = new Uri(url);
                        message.Method = postData == null ? HttpMethod.Get : HttpMethod.Post;

                        if (postData != null)
                            if (postData.ReadAsStringAsync().Result != "")
                                message.Content = postData;


                        if (headers != null)
                        {
                            foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
                            {
                                message.Headers.Add(header.Key, header.Value);
                            }
                        }

                        string data = "";


                        var response = await httpClient.SendAsync(message);
                        data = await response.Content.ReadAsStringAsync();

                       
                        if (!string.IsNullOrEmpty(data))
                        {
                            try
                            {
                                return JsonConvert.DeserializeObject<T>(data);
                            }
                            catch (Exception )
                            {
                                return default(T);
                            }
                        }
  
                        else
                            throw new Exception(errorMessage);
                    }
                }
            }

            throw new Exception(errorMessage);
        }

        private static async Task<string> ProcessRequestStatusAsync(string url, HttpContent postData
                        , List<KeyValuePair<string, IEnumerable<string>>> headers = null, string errorMessage = null)
        {
            using (var handler = new HttpClientHandler())
            {
                //if (handler.SupportsAutomaticDecompression)
                //    handler.AutomaticDecompression = DecompressionMethods.Deflate;

                using (var httpClient = new HttpClient(handler))
                {
                    using (var message = new HttpRequestMessage())
                    {
                        message.RequestUri = new Uri(url);
                        message.Method = postData == null ? HttpMethod.Get : HttpMethod.Post;

                        if (url.StartsWith(AppData.Identity))
                        {
                            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic bW9iaWxlOnNlY3JldA==");
                        }

                        if (headers != null)
                        {
                            foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
                            {
                                message.Headers.Add(header.Key, header.Value);
                            }
                        }

                        if (postData != null)
                            message.Content = postData;

                        string data;
                        try
                        {
                            HttpResponseMessage response = await httpClient.SendAsync(message, CancellationToken.None).ConfigureAwait(false);
                            data = response.StatusCode.ToString();
                        }
                        catch (Exception e)
                        {
                           throw new Exception(errorMessage);
                        }

                        if (!string.IsNullOrEmpty(data))
                            return data;
                        else
                            throw new Exception(errorMessage);
                    }
                }
            }

            throw new Exception(errorMessage);
        }

        #endregion
    }
}

