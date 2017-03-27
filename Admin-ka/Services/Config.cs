using System;
using System.Collections.Generic;

namespace Admin.Services
{
    public class Config : IConfig
    {
        public IReadOnlyList<string> ModuleKey { get; }

        public long WatermarkSpeed { get; }

        public long WatermarkInQ { get; }

        public TimeSpan RefreshInterval { get; }

        public Config(IReadOnlyList<string> types, long watermarkInQ, long watermarkSpeed, TimeSpan refreshInterval)
        {
            ModuleKey = types;
            WatermarkInQ = watermarkInQ;
            WatermarkSpeed = watermarkSpeed;
            RefreshInterval = refreshInterval;
        }
    }
}
