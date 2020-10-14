using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChannelEngine.Assessment.WKettlitz.Shared.DataTransferObjects
{
    public class JsonPatchDocument
    {
        private string? _path;

        public object? Value { get; set; }

        public string Path 
        {
            get => _path ?? throw new InvalidOperationException("Uninitialized " + nameof(Path));
            set => _path = value;
        }

        [JsonPropertyName("op")]
        public JsonPatchOperation Operation { get; set; }
        public string? From { get; set; }
    }
}
