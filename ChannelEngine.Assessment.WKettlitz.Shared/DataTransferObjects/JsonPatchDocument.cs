using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects
{
    public class JsonPatchDocument
    {
        public object Value { get; set; }
        public string Path { get; set; }
        [JsonPropertyName("op")]
        public string Operation { get; set; }
        public string From { get; set; }
    }
}
