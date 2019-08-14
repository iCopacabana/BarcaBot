using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Barcabot.Web
{
    public class DataRetrievalService : IDataRetrievalService
    {
        protected readonly HttpClient Client;

        protected DataRetrievalService(HttpClient http)
            => Client = http;

        public async Task<T> RetrieveData<T>(string url)
        {
            var result = await Client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}