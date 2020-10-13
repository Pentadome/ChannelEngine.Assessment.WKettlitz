using System;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class Secrets
    {
        private string? _apiKey;

        public string ApiKey
        {
            get => _apiKey ?? throw new InvalidOperationException("Uninitialized " + nameof(ApiKey));
            set => _apiKey = value;
        }
    }
}
