using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects.MerchantOrder
{
    public enum OrderStatusView
    {
        [EnumMember(Value = "IN_PROGRESS")]
        InProgress,
        [EnumMember(Value = "SHIPPED")]
        Shipped,
        [EnumMember(Value = "IN_BACKORDER")]
        InBackOrder,
        [EnumMember(Value = "MANCO")]
        Manco,
        [EnumMember(Value = "CANCELED")]
        Cancelled,
        [EnumMember(Value = "IN_COMBI")]
        InCombi,
        [EnumMember(Value = "CLOSED")]
        Closed,
        [EnumMember(Value = "NEW")]
        New,
        [EnumMember(Value = "RETURNED")]
        Returned,
        [EnumMember(Value = "REQUIRES_CORRECTION ")]
        RequiresCorrection
    }
}
