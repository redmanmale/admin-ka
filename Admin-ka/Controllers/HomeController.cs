using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Admin.Model;
using Admin.Services;
using Admin.Utils;
using Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfig _config;
        private readonly IStatService _statService;

        public HomeController(IConfig config, IStatService statService)
        {
            _config = config;
            _statService = statService;
        }

        public async Task<IActionResult> Index()
        {
            var statDict = new Dictionary<string, Task<List<ModuleStat>>>();
            var infoDict = new Dictionary<string, Task<ModuleInfo>>();

            var resDict = new Dictionary<string, Stat>();

            foreach (var type in _config.ModuleKey)
            {
                infoDict.Add(type, _statService.GetInfoAsync(type));
                statDict.Add(type, GetStatAsync(type));
                resDict.Add(type, new Stat());
            }

            foreach (var type in _config.ModuleKey)
            {
                resDict[type].ModuleInfo = await infoDict[type];
                resDict[type].ModuleStat = await statDict[type];
            }

            Response.Headers.Add("Refresh", _config.RefreshInterval.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            return View(new StatViewModel(resDict));
        }

        private async Task<List<ModuleStat>> GetStatAsync(string type)
        {
            var baseDict = await _statService.GetStatsAsync(type);
            return baseDict?.Select(ToInfo).ToList();
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
