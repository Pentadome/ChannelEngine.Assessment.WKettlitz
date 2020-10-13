using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class Config
    {
        private string? _baseApiUrl;

        public string BaseApiUrl
        {
            get => _baseApiUrl ?? throw new InvalidOperationException("Uninitialized " + nameof(BaseApiUrl));
            set => _baseApiUrl = value;
        }

        private string? _getOrdersApiPath;

        public string GetOrdersApiPath
        {
            get => _getOrdersApiPath ?? throw new InvalidOperationException("Uninitialized " + nameof(GetOrdersApiPath));
            set => _getOrdersApiPath = value;
        }
    }
}
