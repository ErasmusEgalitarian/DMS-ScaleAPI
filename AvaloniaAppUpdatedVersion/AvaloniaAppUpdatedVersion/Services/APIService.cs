using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AvaloniaAppUpdatedVersion.Services
{
    public class APIService
    {
        private static readonly HttpClient client = new HttpClient();
        private static string domain = "http://vistimalik.com:5296"; 

        // Method to get the scale version from database via the API

        public async Task Authenticate(string username, string password)
        {
            //var postData = new StringContent("{\"username\": \"" + username + "\", \"password\": \"" + password + "\"}", Encoding.UTF8, "application/JSON");
            var postDict = new Dictionary<string, string> { { "username", username }, { "password", password } };
            var jsonPost = JsonSerializer.Serialize(postDict);
            var postData = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{domain}/api/login", postData);

            var responseString = await response.Content.ReadAsStringAsync();

            var jsonResponseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
            string token = jsonResponseData["token"];

            client.DefaultRequestHeaders.Add("Authorization", token);

        }
        public async Task<string> GetScaleVersion(string scaleID)
        {
            var response = await client.GetAsync($"{domain}/api/getScaleVersion/{scaleID}");
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
            string version = jsonResponseData["message"];
            return version;
        }
        public async Task<string> GetScaleStatus(string scaleID)
        {
            var response = await client.GetAsync($"{domain}/api/getUpDown/{scaleID}");
            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
            string status = jsonResponseData["message"];
            return status;
        }

    }
}
