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
        private readonly IStatRouterService _statRouterService;
        private readonly IReadOnlyList<string> _infoTypeList;

        public HomeController(IConfig config, IStatRouterService statRouterService)
        {
            _statRouterService = statRouterService;
            _infoTypeList = config.Types;
        }

        public async Task<IActionResult> Index()
        {
            var infoDict = _infoTypeList.ToDictionary<string, string, IReadOnlyList<Info>>(type => type, type => new List<Info>());

            foreach (var type in _infoTypeList)
            {
                var baseDict = await _statRouterService.GetStatsAsync(type);
                var list = baseDict.Values.Select(stat => stat.IsSpeed ? new Info
                {
                    Name = stat.Name,
                    Value1 = $"SpeedInstant: {stat.SpeedIns.FormatSize()}",
                    Value2 = $"SpeedAverage: {stat.SpeedAvg.FormatSize()}"
                } : new Info
                {
                    Name = stat.Name,
                    Value1 = $"All: {stat.CountAll}",
                    Value2 = $"InQ: {stat.CountInQ}"
                }).ToList();

                infoDict[type] = list;
            }

            return View(new InfoViewModel(infoDict));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
