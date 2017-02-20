using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Admin.Model;
using Newtonsoft.Json;

namespace Admin.Services
{
    public class StatRouterService : IStatRouterService
    {
        private readonly IReadOnlyDictionary<string, HttpClient> _clientDict;

        public StatRouterService(IReadOnlyDictionary<string, Uri> uriDict)
        {
            _clientDict = uriDict.ToDictionary(uri => uri.Key, uri => new HttpClient { BaseAddress = uri.Value, Timeout = TimeSpan.FromSeconds(10) });
        }

        public async Task<IReadOnlyDictionary<string, StatContract>> GetStatsAsync(string type)
        {
            try
            {
                var result = await _clientDict[type].GetAsync("stats").ConfigureAwait(false);
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyDictionary<string, StatContract>>(content);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }
    }
}
