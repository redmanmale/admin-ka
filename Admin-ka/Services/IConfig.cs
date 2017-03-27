using System;
using System.Collections.Generic;

namespace Admin.Services
{
    public interface IConfig
    {
        IReadOnlyList<string> ModuleKey { get; }

        long WatermarkSpeed { get; }

        long WatermarkInQ { get; }

        TimeSpan RefreshInterval { get; }
    }
}
