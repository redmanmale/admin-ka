using System;

namespace Admin.Utils
{
    public static class MetricExtension
    {
        public static bool IsMetricInDanger<T>(this T metricValue, T watermark, WatermarkType watermarkType) where T : IComparable
        {
            switch (watermarkType)
            {
                case WatermarkType.More:
                    return metricValue.CompareTo(watermark) == 1;
                case WatermarkType.Less:
                    return metricValue.CompareTo(watermark) == -1;
                case WatermarkType.MoreOrEqual:
                    return metricValue.CompareTo(watermark) > -1;
                case WatermarkType.LessOrEqual:
                    return metricValue.CompareTo(watermark) < 1;
                default:
                    throw new ArgumentOutOfRangeException(nameof(watermarkType), watermarkType, null);
            }
        }
    }
}