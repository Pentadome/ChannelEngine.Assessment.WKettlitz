using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantOrder;
using Microsoft.Extensions.Configuration;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class OrdersApi
    {
        private readonly HttpClient _httpClient;
        private readonly Config _config;
        private readonly Secrets _secrets;
        

        public OrdersApi(HttpClient httpClient, Config config, Secrets secrets)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _secrets = secrets ?? throw new ArgumentNullException(nameof(secrets));
        }

        public async Task<CollectionOfMerchantOrderResponse> GetInProgressOrdersAsync(CancellationToken cancellationToken = default)
        {
            var requestUriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = _config.BaseApiUrl,
                Path = _config.OrdersApiPath,
                Query = $"?apikey={_secrets.ApiKey}&statuses=IN_PROGRESS"
            };

            var response = await _httpClient.GetAsync(requestUriBuilder.Uri, cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response
                .DeserializeResponseAsync<CollectionOfMerchantOrderResponse>(Constants.JsonSerializerOptions, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
