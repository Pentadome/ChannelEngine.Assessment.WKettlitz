using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantProduct
{
    public class CollectionOfMerchantProductResponse
    {
        private List<MerchantProductResponse>? _content;

        public List<MerchantProductResponse> Content
        {
            get => _content ??= new List<MerchantProductResponse>();
            set => _content = value;
        }
    }
}
