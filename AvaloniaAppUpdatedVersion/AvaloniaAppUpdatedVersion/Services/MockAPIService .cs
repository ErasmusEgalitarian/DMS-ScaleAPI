using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace AvaloniaAppUpdatedVersion.Services
{
    // Mock API service to simulate API calls and data retrieval
    public class MockAPIService
    {
        private static readonly HttpClient client = new HttpClient();
        private static string domain = "http://vistimalik.com:5296";


        // Method to get the scale version from database via the API
        public async void Authenticate(string username, string password)
        {
            //var postData = new StringContent("{\"username\": \"" + username + "\", \"password\": \"" + password + "\"}", Encoding.UTF8, "application/JSON");
            var postDict = new Dictionary<string, string> { { "username", username }, { "password", password } };
            var jsonPost = JsonSerializer.Serialize(postDict);
            var postData = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            // var response = await client.PostAsync($"{domain}/api/login", postData);


            // string response = "{\"token\": \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3YXN0ZXdvcmtlciIsImlhdCI6MTY5NTQ2NzA4MCwiZXhwIjoxNjk1NDY3MTQwfQ.8f\"}";
            // response.Content = new StringContent("{\"token\": \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3YXN0ZXdvcmtlciIsImlhdCI6MTY5NTQ2NzA4MCwiZXhwIjoxNjk1NDY3MTQwfQ.8f\"}");
            // var responseString = response.Content;


            // var jsonResponseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
            string token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ3YXN0ZXdvcmtlciIsImlhdCI6MTY5NTQ2NzA4MCwiZXhwIjoxNjk1NDY3MTQwfQ.8f";

            client.DefaultRequestHeaders.Add("Authorization", token);

        }
        public async Task<string> GetScaleVersion(int callCount)
        {
            
        // var response = await client.GetAsync($"{domain}/api/getScaleVersion/{scaleID}");
        // var responseString = await response.Content.ReadAsStringAsync();
        // var jsonResponseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
        // string version = jsonResponseData["message"];
        string version = $"1.0.{callCount}"; // Mock version
            return version;
        }
        public async Task<string> GetScaleStatus(int callCount)
        {
            
             // Hvis du i fremtiden vil bruge en rigtig HTTP-request, kan du gøre det her
             // var response = await client.GetAsync($"{domain}/api/getUpDown/{scaleID}");
             // var responseString = await response.Content.ReadAsStringAsync();
             // var jsonResponseData = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
             // string status = jsonResponseData["message"];

             // Mock-logik: Returnér "up" hvis callCount <= 10, ellers "down"
             string status = callCount <= 10 ? "Up" : "Down";
             return status;
            
        }

    }
}
