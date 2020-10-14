using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ChannelEngine.Assessment.WKettlitz.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace ChannelEngine.Assessment.WKettlitz.ConsoleApp
{
    class Program
    {
        private static readonly Secrets _secrets = new Secrets
        {
            ApiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322"
        };

        private static readonly Config _config = new Config
        {
            BaseApiUrl = "api-dev.channelengine.net",
            OrdersApiPath = "api/v2/orders",
            ProductsApiPath = "api/v2/products"
        };

        private static readonly HttpClient _httpClient = new HttpClient();

        private static readonly Random _random = new Random();

        static async Task Main(string[] args)
        {
            Console.WriteLine("press enter to start");
            _ = Console.ReadLine();

            var ordersApi = new OrdersApi(_httpClient, _config, _secrets);

            var orders = await ordersApi.GetInProgressOrdersAsync().ConfigureAwait(false);
            Console.WriteLine($"Fetched {orders.Content.Count} orders that are in progress.");

            Console.WriteLine("Calculation top 5 products sold");

            var orderBll = new OrdersBll();

            var topFiveSold = orderBll
                .OrderProductsByQuantity(orders)
                .Take(5)
                .ToList();

            Console.WriteLine("Getting product names...");

            var productsAPi = new ProductsApi(_httpClient, _config, _secrets);

            foreach (var (MerchantProductNo, Gtin, Sold) in topFiveSold)
            {
                var product = (await productsAPi.GetProductAsync(MerchantProductNo!).ConfigureAwait(false)).Content.First();
                Console.WriteLine($"Sold: {Sold}, No: {MerchantProductNo}, Gtin: {Gtin}, Stock: {product.Stock}, Name: {product.Name}");
            }

            Console.WriteLine("Updating random product stock to 25...");

            var randomProductIndexToUpdate = _random.Next(0, topFiveSold.Count);
            var randomProduct = topFiveSold.ElementAt(randomProductIndexToUpdate);

            var success = await productsAPi.UpdateProductStockQuantityAsync(randomProduct.MerchantProductNo!, 25);

            Console.WriteLine($"Success: {success}");
            Console.WriteLine("Press enter to close");
            _ = Console.ReadLine();
        }
    }
} 
