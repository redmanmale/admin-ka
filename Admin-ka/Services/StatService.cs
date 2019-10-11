using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Admin.Model;
using Admin.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Admin.Services
{
    public class StatService : IHostedService
    {
        private readonly IReadOnlyDictionary<string, HttpClient> _clientDict;
        private readonly IConfig _config;
        private readonly IHubContext<AdminHub> _hubContext;

        public StatService(IReadOnlyDictionary<string, Uri> uriDict, IConfig config, IHubContext<AdminHub> hubContext)
        {
            _clientDict = uriDict.ToDictionary(uri => uri.Key, uri => new HttpClient { BaseAddress = uri.Value, Timeout = TimeSpan.FromSeconds(10) });
            _config = config;
            _hubContext = hubContext;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var statDict = new Dictionary<string, Task<List<ModuleStat>>>();
                var infoDict = new Dictionary<string, Task<ModuleInfo>>();

                var resDict = new Dictionary<string, Stat>();

                foreach (var type in _config.ModuleKey)
                {
                    infoDict.Add(type, GetInfoAsync(type));
                    statDict.Add(type, GetStatAsync(type));
                    resDict.Add(type, new Stat());
                }

                foreach (var type in _config.ModuleKey)
                {
                    resDict[type].ModuleInfo = await infoDict[type];
                    resDict[type].ModuleStat = await statDict[type];
                }

                await _hubContext.Clients.All.SendAsync("pushStats", resDict);

                await Task.Delay(_config.RefreshInterval);
            }
        }

        private async Task<List<ModuleStat>> GetStatAsync(string type)
        {
            var baseDict = await GetStatsAsync(type);
            return baseDict?.Select(ToInfo).ToList();
        }

        private async Task<StatContract[]> GetStatsAsync(string type)
        {
            try
            {
                var result = await _clientDict[type].GetAsync("stats").ConfigureAwait(false);
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<StatContract[]>(content);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        private async Task<ModuleInfo> GetInfoAsync(string type)
        {
            try
            {
                var result = await _clientDict[type].GetAsync("info").ConfigureAwait(false);
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ModuleInfo>(content);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        private ModuleStat ToInfo(StatContract stat)
        {
            switch (stat.StatMode)
            {
                case StatMode.Speed:
                    {
                        return new ModuleStat
                        {
                            Name = stat.Name,
                            Content = FormatMetrics(stat.Metrics, true),
                            IsDanger = stat.Metrics["SpeedNow"].IsMetricInDanger(_config.WatermarkSpeed, WatermarkType.LessOrEqual)
                        };
                    }
                case StatMode.Count:
                    {
                        return new ModuleStat
                        {
                            Name = stat.Name,
                            Content = FormatMetrics(stat.Metrics),
                            IsDanger = stat.Metrics["InQ"].IsMetricInDanger(_config.WatermarkInQ, WatermarkType.More)
                        };
                    }
                default:
                    return null;
            }
        }

        private static string FormatMetrics(IReadOnlyDictionary<string, long> metrics, bool format = false)
        {
            return string.Join(" | ", metrics.Select(pair => $"{pair.Key}: {(format ? pair.Value.FormatSize() : pair.Value.ToString())}"));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_clientDict != null)
            {
                foreach (var item in _clientDict)
                {
                    item.Value.Dispose();
                }
            }

            return Task.CompletedTask;
        }
    }
}
