using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Admin.Controllers;
using Admin.Model;
using Newtonsoft.Json;

namespace Admin.Services
{
    public class StatRouterService : IStatRouterService
    {
        private readonly IReadOnlyDictionary<string, HttpClient> _clientDict;

        public StatRouterService(IReadOnlyDictionary<string, Uri> uriDict)
        {
            _clientDict = uriDict.ToDictionary(uri => uri.Key, uri => new HttpClient { BaseAddress = uri.Value });
        }

        public async Task<IReadOnlyDictionary<string, StatContract>> GetStatsAsync(string type)
        {
            var result = await _clientDict[type].GetAsync("stats");
            var content = await result.Content.ReadAsStringAsync();
            var dict = JsonConvert.DeserializeObject<IReadOnlyDictionary<string, StatContract>>(content);

            return dict;
        }
    }
}
