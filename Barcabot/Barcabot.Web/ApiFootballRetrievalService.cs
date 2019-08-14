using System;
using System.Net.Http;
using Barcabot.Common;

namespace Barcabot.Web
{
    public class ApiFootballRetrievalService : DataRetrievalService
    {
        public ApiFootballRetrievalService(HttpClient http) : base(http)
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
            Client.DefaultRequestHeaders.Add("X-RapidAPI-Key", YamlConfiguration.Config.ApiTokens.ApiFootball);
        }
    }
}