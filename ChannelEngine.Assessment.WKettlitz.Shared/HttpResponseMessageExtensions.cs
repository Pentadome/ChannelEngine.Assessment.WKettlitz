using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelEngine.Assessment.WKettlitz.Shared
{
    public static class HttpResponseMessageExtensions
    {
        public async static Task<T> DeserializeResponse<T>(this HttpResponseMessage @this, JsonSerializerOptions jsonSerializerOptions, CancellationToken cancellationToken = default)
        {
            if (@this is null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var jsonStream = await @this.Content.ReadAsStreamAsync().ConfigureAwait(false);

            Debug.WriteLine(await @this.Content.ReadAsStringAsync());

            return await JsonSerializer
                .DeserializeAsync<T>(jsonStream, jsonSerializerOptions, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
