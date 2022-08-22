using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsumeGraphAPI.Services
{
    public class MainServices : IMainServices
    {
        public async Task<string> GetAuthorizeToken()
        {
            string _accesstoken = string.Empty;
            string _tokenUrl = "https://login.windows.net/token";
            string _client_id = "58ccb6aa-e9ad-431d-8ee4-f361f6b40b18";
            string _client_secret = "9638f585-4652-44aa-9cf5-6da11f42071d";
            string _resource = "https://test.dynamics.com/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_tokenUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
                var postValue = new List<KeyValuePair<string, string>>();
                postValue.Add(new KeyValuePair<string, string>("resource", _resource));
                postValue.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                postValue.Add(new KeyValuePair<string, string>("client_id", _client_id));
                postValue.Add(new KeyValuePair<string, string>("client_secret", _client_secret));
                HttpContent requestParams = new FormUrlEncodedContent(postValue);
                response = await client.PostAsync(_tokenUrl, requestParams).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string tokenresponseObj = response.Content.ReadAsStringAsync().Result;
                    dynamic tokenObject = JsonConvert.DeserializeObject<dynamic>(tokenresponseObj);
                    _accesstoken = tokenObject["access_token"];
                }
            }
            return _accesstoken;
        }
        public async Task<string> GetDataByAuthToken()
        {
            string respData = "";
            try
            {
                string endpointURL = "https://graphapiurl";
                string _getToken = await GetAuthorizeToken();
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _getToken);
                    client.BaseAddress = new Uri(endpointURL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(endpointURL).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string DetailsresponseObj = response.Content.ReadAsStringAsync().Result;
                        respData = JsonConvert.DeserializeObject<object>(DetailsresponseObj).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return respData;
        }
    }
}
