using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SastanaArt.APIModels.ResponseModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SastanaArt.Helpers
{
    internal class ServiceClient
    {
        public enum MethodType
        {

        }

        static HttpClient client;
        static ServiceClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.BaseURL);
        }

        public async static Task<R> PostAsync<T, R>(T model,string apiName)
        {
            #region Commented Code for Auth using httpClient
            try
            {
                string json = JsonConvert.SerializeObject(model);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiName, body);

                if (response != null)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<R>(result);
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject<R>("");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong" ,ex);
            }
            
            //return null;
            #endregion
        }

        public async static Task<LoginResponse> AuthenticateAsync<T>(T model)
        {
            #region Commented Code for Auth using httpClient
            
            var loginData = await PostAsync<object, LoginResponse>(model, Constants.LoginAPI);
            
            if (loginData != null)
            {
                if(loginData.code == 200)
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginData.data.token}");
            }

            return loginData;
            #endregion
        }

        public static void ClearToken()
        {
            client.DefaultRequestHeaders.Clear();
        }
    }
}
