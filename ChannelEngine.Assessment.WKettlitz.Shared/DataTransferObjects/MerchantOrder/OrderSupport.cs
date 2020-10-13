using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantOrder
{
    public enum OrderSupport
    {
        [EnumMember(Value = "NONE")]
        None,
        [EnumMember(Value = "ORDERS")]
        Orders,
        [EnumMember(Value = "SPLIT_ORDERS")]
        SplitOrders,
        [EnumMember(Value = "SPLIT_ORDER_LINES")]
        SplitOrderLines
    }
}
