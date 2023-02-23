using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace lab5_pppi
{
    internal class UnsplashAPIController
    {
        private readonly HttpClient _httpClient;
        private string accessKey;
        private string secretKey;
        static private string baseUrl = "https://api.unsplash.com/";

        public UnsplashAPIController(string accessKey, string secretKey)
        {
            _httpClient = new HttpClient();
            this.accessKey = accessKey;
            this.secretKey = secretKey;
            try
            {
                string token = GetToken().Result;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            } catch(Exception ex) {
                Console.WriteLine("Error: Invalid credentials, you're allowed to do only public actions.");
            }
        }
        public async Task<Response> GetRandomPhoto(string query)
        {
            string url = baseUrl + $"photos/random?client_id={accessKey}"
            + $"&query={query}";
            HttpResponseMessage apiResponse;
            string response;
            Response photoResponse;
            try { 
                apiResponse = await _httpClient.GetAsync(url);
                response = await apiResponse.Content.ReadAsStringAsync();
                HttpStatusCode statusCode = apiResponse.StatusCode;
                HttpRequestMessage message = apiResponse.RequestMessage;
                string output = JsonConvert.DeserializeObject(response).ToString();
                Photo photoData = JsonConvert.DeserializeObject<Photo>(output);

                photoResponse = new Response(statusCode, message, photoData);
            } catch (Exception e)
            {
                photoResponse = new Response();
            }
            return photoResponse;
        }
        public async Task<Response> LikePhoto(string id)
        {
            string url = baseUrl + $"photos/{id}/like/?client_id={accessKey}";
            var httpContent = new StringContent("");
            Response photoResponse;
            string response;
            try
            {
                HttpResponseMessage apiResponse = await _httpClient.PostAsync(url, httpContent);
                response = await apiResponse.Content.ReadAsStringAsync();
                HttpStatusCode statusCode = apiResponse.StatusCode;
                HttpRequestMessage message = apiResponse.RequestMessage;
                string output = JsonConvert.DeserializeObject(response).ToString();
                Photo photoData = JsonConvert.DeserializeObject<Photo>(output);

                photoResponse = new Response(statusCode, message, photoData);
            } catch (Exception e)
            {
                photoResponse = new Response();
            }
            return photoResponse;
        }
        public static async Task<string> GetAccessToken(string clientId, string clientSecret, string redirectUri, string code)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}")));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("code", code)
            });

            var response = await httpClient.PostAsync("https://unsplash.com/oauth/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseJson = JObject.Parse(responseContent);
                return responseJson.GetValue("access_token").ToString();
            }
            else
            {
                throw new Exception($"Failed to get access token. " +
                    $"Response status code: {response.StatusCode}. " +
                    $"Response content: {responseContent}");
            }
        }
        public async Task<string> GetToken()
        {
            Console.WriteLine("Hello! Go to the https://unsplash.com/oauth/authorize with your credentials " +
                "and enter your auth code: ");
            string code = Console.ReadLine();
            var token = GetAccessToken(this.accessKey, this.secretKey, "urn:ietf:wg:oauth:2.0:oob", code).Result;      
            return token;
        }
    }
}
