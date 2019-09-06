using System;
using System.Net.Http;
using Barcabot.Common;

namespace Barcabot.Web
{
    public class FootballDataApiRetrievalService : DataRetrievalService
    {
        public FootballDataApiRetrievalService(HttpClient http) : base(http)
        {
            Client.DefaultRequestHeaders.Add("X-Auth-Token", YamlConfiguration.Config.Apis.FootballData.Token);
        }
    }
}