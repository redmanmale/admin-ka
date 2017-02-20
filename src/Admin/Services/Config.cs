using System.Collections.Generic;

namespace Admin.Services
{
    public class Config : IConfig
    {
        public IReadOnlyList<string> Types { get; }

        public long WatermarkSpeed { get; }

        public long WatermarkInQ { get; }

        public Config(IReadOnlyList<string> types, long watermarkInQ, long watermarkSpeed)
        {
            Types = types;
            WatermarkInQ = watermarkInQ;
            WatermarkSpeed = watermarkSpeed;
        }
    }
}
