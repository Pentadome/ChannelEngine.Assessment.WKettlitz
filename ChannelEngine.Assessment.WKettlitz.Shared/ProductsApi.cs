using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class ProductsApi
    {
        private readonly HttpClient _httpClient;
        private readonly Config _config;
        private readonly Secrets _secrets;


        public ProductsApi(HttpClient httpClient, Config config, Secrets secrets)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _secrets = secrets ?? throw new ArgumentNullException(nameof(secrets));
        }

        public async Task<bool> UpdateProductStockQuantityAsync(string merchantProductNo, int stockQuantity, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(merchantProductNo))
                throw new ArgumentException($"'{nameof(merchantProductNo)}' cannot be null or whitespace", nameof(merchantProductNo));

            if (stockQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(stockQuantity));

            var patchDocument = new JsonPatchDocument
            {
                Path = "Stock",
                Operation = "replace",
                Value = stockQuantity
            };

            var uriBuilder = new UriBuilder
            {
                Host = _config.BaseApiUrl,
                Path = _config.GetPatchProductsUriPath(merchantProductNo),
                Query = $"?apikey={_secrets.ApiKey}"
            };

            var utf8Stream = new MemoryStream();
            await JsonSerializer
                    .SerializeAsync(utf8Stream, patchDocument, Constants.JsonSerializerOptions, cancellationToken)
                    .ConfigureAwait(false);

            var streamContent = new StreamContent(utf8Stream);

            var response = await _httpClient.PatchAsync(uriBuilder.Uri, streamContent, cancellationToken)
                                            .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}
