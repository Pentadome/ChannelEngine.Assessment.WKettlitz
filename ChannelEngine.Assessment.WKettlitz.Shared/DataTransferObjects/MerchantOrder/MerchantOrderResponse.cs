using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantOrder
{
    class MerchantOrderResponse
    {
        public int Id { get; set; }
        public string? ChannelName { get; set; }
        public int? ChannelId { get; set; }
        public string? GlobalChannelName { get; set; }
        public int? GlobalChannelId { get; set; }
        public OrderSupport ChannelOrderSupport { get; set; }
        public string? ChannelOrderNo { get; set; }
        public OrderStatusView Status { get; set; }
        public bool IsBusinessOrder { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? MerchantComment { get; set; }

    }
}
