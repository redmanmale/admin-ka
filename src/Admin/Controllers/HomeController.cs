using System.Collections.Generic;
using Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<string> _infoTypeList;

        public HomeController()
        {
            _infoTypeList = new List<string> { "source", "router", "proxy", "import" };
        }

        public IActionResult Index()
        {
            var infoDict = new Dictionary<string, IReadOnlyList<Info>>();
            foreach (var infoType in _infoTypeList)
            {
                var sourceInfos = new List<Info>
                {
                    new Info
                    {
                        Name = "AdsIn",
                        Value1 = "0 b/s",
                        Value2 = "104 b/s"
                    },
                    new Info
                    {
                        Name = "Filter",
                        Value1 = "1345",
                        Value2 = "83",
                        Mode = 2
                    }
                };
                infoDict.Add(infoType, sourceInfos.AsReadOnly());
            }

            return View(new InfoViewModel(infoDict));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
