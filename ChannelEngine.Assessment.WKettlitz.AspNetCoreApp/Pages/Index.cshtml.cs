using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngine.Assessment.WKettlitz.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ChannelEngine.Assessment.WKettlitz.AspNetCoreApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly OrdersApi _ordersApi;
        private readonly OrdersBll _ordersBll;
        private readonly ProductsApi _productsApi;

        public IndexModel(ILogger<IndexModel> logger, OrdersApi ordersApi, OrdersBll ordersBll, ProductsApi productsApi)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ordersApi = ordersApi ?? throw new ArgumentNullException(nameof(ordersApi));
            _ordersBll = ordersBll ?? throw new ArgumentNullException(nameof(ordersBll));
            _productsApi = productsApi ?? throw new ArgumentNullException(nameof(productsApi));
        }

        public List<ProductInfoViewModel> Products { get; } = new List<ProductInfoViewModel>();

        public async Task OnGetAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting Orders In Progress...");
            var orders = await _ordersApi.GetInProgressOrdersAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("Orders received. Calculation top 5 products...");
            var topFiveProducts = _ordersBll.OrderProductsByQuantity(orders).ToList();
            _logger.LogInformation("Getting Product names...");

            foreach (var (MerchantProductNo, Gtin, Sold) in topFiveProducts)
            {
                var product = (await _productsApi.GetProductAsync(MerchantProductNo!).ConfigureAwait(false)).Content.First();
                Products.Add(new ProductInfoViewModel
                {
                    Gtin = Gtin,
                    MerchantProductNo = MerchantProductNo,
                    Name = product.Name,
                    Sold = Sold,
                    Stock = product.Stock
                });
            }
            _logger.LogInformation("Done...");
        }

        public class ProductInfoViewModel
        {
            public string MerchantProductNo { get; set; }
            public string Gtin { get; set; }
            public int Sold { get; set; }
            public int Stock { get; set; }
            public string Name { get; set; }
        }
    }
}
