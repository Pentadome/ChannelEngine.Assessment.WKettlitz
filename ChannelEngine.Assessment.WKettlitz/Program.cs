using System;
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
            BaseApiUrl = "https://api-dev.channelengine.net/",
            GetOrdersApiPath = "api/v2/orders"
        };

        static async Task Main(string[] args)
        {
            var ordersApi = new OrdersApi(new HttpClient(), _config, _secrets);
        }

        private static int GetChoice()
        {
            Console.WriteLine("1: Fetch all orders with status IN_PROGRESS from the API");
            var inputString = Console.ReadLine();

            return int.TryParse(inputString, out var result) ? result : GetChoice();
        }
    }
}
