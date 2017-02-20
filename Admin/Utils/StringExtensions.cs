using ByteSizeLib;

namespace Admin.Utils
{
    public static class StringExtensions
    {
        public static string FormatSize(this long number) => ByteSize.FromBytes(number) + "/s";
    }
}
