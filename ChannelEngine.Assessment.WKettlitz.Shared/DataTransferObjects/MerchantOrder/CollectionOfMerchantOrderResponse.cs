using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantOrder
{
    public class CollectionOfMerchantOrderResponse
    {
        private List<MerchantOrderResponse>? _content;

        public List<MerchantOrderResponse> Content
        {
            get => _content ??= new List<MerchantOrderResponse>();
            set => _content = value;
        }
    }
}
