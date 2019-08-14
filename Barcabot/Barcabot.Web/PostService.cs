using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Barcabot.Common.DataModels;
using Newtonsoft.Json;

namespace Barcabot.Web
{
    public class PostService
    {
        private readonly HttpClient _client;

        public PostService(HttpClient http)
            => _client = http;

        public async Task<PostStreamResponse> GetStreamFromPost<T>(string url, T requestBody)
        {
            var jsonString = JsonConvert.SerializeObject(requestBody);
            var response = await _client.PostAsync(url, new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseCode = response.StatusCode.ToString();
            var responseContent = await response.Content.ReadAsStreamAsync();

            return new PostStreamResponse
            {
                ResponseCode = responseCode,
                ResponseContent = responseContent
            };
        }
        
        public async Task<string> GetStringFromPost<T>(string url, T requestBody)
        {
            var jsonString = JsonConvert.SerializeObject(requestBody);
            var response = await _client.PostAsync(url, new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}