using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class OrdersApi
    {
        private readonly HttpClient _httpClient;

        public OrdersApi(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public 
    }
}
