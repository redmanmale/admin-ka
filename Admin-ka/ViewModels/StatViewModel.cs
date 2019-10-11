using System.Collections.Generic;
using System.Linq;
using Admin.Model;

namespace Admin.ViewModels
{
    public class StatViewModel
    {
        private readonly IReadOnlyDictionary<string, Stat> _statDict;

        public IReadOnlyList<string> GetModuleKeys => _statDict.Keys.OrderByDescending(f => f).ToList();

        public IReadOnlyList<ModuleStat> GetModuleStatByKey(string type) => _statDict[type].ModuleStat ?? new List<ModuleStat>(0);

        public string GetModuleNameByKey(string type) => _statDict[type].ModuleInfo?.ToString();

        public StatViewModel(IReadOnlyDictionary<string, Stat> statDict)
        {
            _statDict = statDict;
        }

        public StatViewModel()
        {
        }
    }
}
