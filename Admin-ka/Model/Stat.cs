using System.Collections.Generic;

namespace Admin.Model
{
    public class Stat
    {
        public ModuleInfo ModuleInfo { get; set; }

        public IReadOnlyList<ModuleStat> ModuleStat { get; set; }
    }
}
