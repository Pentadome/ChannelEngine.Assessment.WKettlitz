using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantOrder;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public class OrdersBll
    {
        public IOrderedEnumerable<(string? MerchantProductNo, string? Gtin, int Quantity)> OrderProductsByQuantity(CollectionOfMerchantOrderResponse orders)
        {
            if (orders is null)
            {
                throw new ArgumentNullException(nameof(orders));
            }

            return orders.Content
                .SelectMany(x => x.Lines)
                .GroupBy(x => x.MerchantProductNo)
                .Select(x => (MerchantProductNo: x.Key, Gtin: x.Select(x => x.Gtin).First(), Quantity: x.Sum(x => x.Quantity)))
                .OrderByDescending(x => x.Quantity);
        }
    }
}
