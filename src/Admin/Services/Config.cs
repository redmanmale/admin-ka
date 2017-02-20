using System.Collections.Generic;

namespace Admin.Services
{
    public class Config : IConfig
    {
        public IReadOnlyList<string> Types { get; }

        public Config(IReadOnlyList<string> types)
        {
            Types = types;
        }
    }
}
