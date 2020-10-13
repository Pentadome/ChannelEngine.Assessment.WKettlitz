using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantOrder
{
    public class MerchantOrderLineResponse
    {
        public string? MerchantProductNo { get; set; }
        public string? Gtin { get; set; }
        public int Quantity { get; set; }
    }
}
