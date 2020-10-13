using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    internal static class Constants
    {
        public static JsonSerializerOptions JsonSerializerOptions { get; } = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
#if DEBUG
            WriteIndented = true,
#endif
        };
    }
}
