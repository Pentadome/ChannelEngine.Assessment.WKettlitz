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
using ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantProduct;

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
                Operation = JsonPatchOperation.Replace,
                Value = stockQuantity
            };

            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = _config.BaseApiUrl,
                Path = _config.GetPatchProductsUriPath(merchantProductNo),
                Query = $"?apikey={_secrets.ApiKey}"
            };

#if DEBUG
            var serializedString = JsonSerializer.Serialize(patchDocument, Constants.JsonSerializerOptions);
            Console.WriteLine(serializedString);
#endif

            var utf8Stream = new MemoryStream();
            await JsonSerializer
                    .SerializeAsync(utf8Stream, patchDocument, Constants.JsonSerializerOptions, cancellationToken)
                    .ConfigureAwait(false);

            var streamContent = new StreamContent(utf8Stream);

            // Not very familiar with JSon patch
            var response = await _httpClient.PatchAsync(uriBuilder.Uri, streamContent, cancellationToken)
                                            .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<CollectionOfMerchantProductResponse> GetProductAsync(string merchantProductNo, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(merchantProductNo))
            {
                throw new ArgumentException($"'{nameof(merchantProductNo)}' cannot be null or whitespace", nameof(merchantProductNo));
            }

            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = _config.BaseApiUrl,
                Path = _config.ProductsApiPath,
                Query = $"?apikey={_secrets.ApiKey}&search={merchantProductNo}"
            };

            var response = await _httpClient.GetAsync(uriBuilder.Uri, cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response
                .DeserializeResponseAsync<CollectionOfMerchantProductResponse>(Constants.JsonSerializerOptions, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
