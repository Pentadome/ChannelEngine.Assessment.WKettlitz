using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class Config
    {
        private string? _baseApiUrl;
        private string? _ordersApiPath;
        private string? _productsApiPath;

        public string BaseApiUrl
        {
            get => _baseApiUrl ?? throw new InvalidOperationException("Uninitialized " + nameof(BaseApiUrl));
            set => _baseApiUrl = value;
        }

        public string OrdersApiPath
        {
            get => _ordersApiPath ?? throw new InvalidOperationException("Uninitialized " + nameof(OrdersApiPath));
            set => _ordersApiPath = value;
        }

        public string ProductsApiPath
        {
            get => _productsApiPath ?? throw new InvalidOperationException("Uninitialized " + nameof(ProductsApiPath));
            set => _productsApiPath = value;
        }

        public string GetPatchProductsUriPath(string merchantProductNo)
        {
            return $"{ProductsApiPath}/{merchantProductNo}";
        }
    }
}
