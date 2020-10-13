using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantOrder
{
    public class MerchantOrderResponse
    {
        private List<MerchantOrderLineResponse>? _lines;

        public OrderStatusView Status { get; set; }

        public List<MerchantOrderLineResponse> Lines
        { 
            get => _lines ??= new List<MerchantOrderLineResponse>();
            set => _lines = value;
        }
    }
}
