﻿using System.Collections.Concurrent;
using System.Collections.Generic;
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
            var cdict = new ConcurrentDictionary<string, IReadOnlyList<Info>>();

            var tasks = _config.Types.Select(async type => cdict[type] = await GetStatAsync(type));
            await Task.WhenAll(tasks);

            return View(new InfoViewModel(cdict));
        }

        private async Task<List<Info>> GetStatAsync(string type)
        {
            var baseDict = await _statService.GetStatsAsync(type);
            return baseDict?.Values?.Select(ToInfo).ToList();
        }

        private Info ToInfo(StatContract stat)
        {
            return stat.IsSpeed
                ? new Info
                {
                    Name = stat.Name,
                    Value1 = $"SpeedInstant: {stat.SpeedIns.FormatSize()}",
                    Value2 = $"SpeedAverage: {stat.SpeedAvg.FormatSize()}",
                    IsDanger = stat.SpeedIns <= _config.WatermarkSpeed
                }
                : new Info
                {
                    Name = stat.Name,
                    Value1 = $"All: {stat.CountAll}",
                    Value2 = $"InQ: {stat.CountInQ}",
                    IsDanger = stat.CountInQ >= _config.WatermarkInQ
                };
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}