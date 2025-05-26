using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace AvaloniaAppUpdatedVersion.Services
{
    public class APIService
    {
        private static readonly HttpClient client = new HttpClient();
        private static string domain = "http://vistimalik.com:5296";
        private string StatusMessage = "";

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

        public async Task<string> UploadFirmware(string SelectedFilePath)
        {
            if (string.IsNullOrEmpty(SelectedFilePath)) return (StatusMessage = "File Path is null or empty"); // Should be redundant, but just in case

            else
            {
                try
                {
                    byte[] fileBytes = await File.ReadAllBytesAsync(SelectedFilePath);

                    var content = new ByteArrayContent(fileBytes);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                    var response = await client.PostAsync($"{domain}/api/uploadFirmware", content);
                    StatusMessage = response.IsSuccessStatusCode ? "Upload successful!" : "Upload failed.";
                }
                catch (Exception ex)
                {
                    int maxLineLength = 150;
                    string errorMessage = ex.Message;

                    if (errorMessage.Length > maxLineLength)
                    {
                        int breakIndex = errorMessage.LastIndexOf(' ', maxLineLength); // Tries to break line a bit earlier at a space
                        if (breakIndex == -1) breakIndex = maxLineLength; // If no space found, break at max length

                        errorMessage = errorMessage.Insert(breakIndex, "\n");
                    }

                    StatusMessage = $"Error: {errorMessage}";
                }
                return StatusMessage;
            }
        }

    }
}
