using System.Collections.Generic;

namespace Admin.Services
{
    public interface IConfig
    {
        IReadOnlyList<string> Types { get; }

        long WatermarkSpeed { get; }

        long WatermarkInQ { get; }
    }
}
