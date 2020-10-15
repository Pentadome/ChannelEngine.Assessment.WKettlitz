using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class Config
    {
#nullable disable

        public string BaseApiUrl { get; set; }

        public string OrdersApiPath { get; set; }

        public string ProductsApiPath { get; set; }
      

        public string GetPatchProductsUriPath(string merchantProductNo)
        {
            return $"{ProductsApiPath}/{merchantProductNo}";
        }
    }
}
