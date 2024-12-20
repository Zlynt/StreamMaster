﻿using System.IO.Pipelines;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using StreamMaster.Streams.Domain.Statistics;

namespace StreamMaster.Streams.Domain.Interfaces
{
    /// <summary>
    /// Represents the configuration for a client, including details such as the client's IP address, user agent, and associated stream channel.
    /// </summary>
    public interface IClientConfiguration
    {
        StreamHandlerMetrics Metrics { get; }
        TaskCompletionSource<bool> CompletionSource { get; }
        event EventHandler ClientStopped;
        /// <summary>
        /// Gets the unique identifier for the current HTTP context.
        /// </summary>
        string HttpContextId { get; }

        /// <summary>
        /// Gets the cancellation Token associated with the client.
        /// </summary>
        /// [IgnoreMember]
        [JsonIgnore]
        CancellationToken ClientCancellationToken { get; set; }

        /// <summary>
        /// Gets or sets the SMChannel associated with the client.
        /// </summary>
        SMChannelDto SMChannel { get; set; }

        ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// Gets or sets the client's IP address.
        /// </summary>
        string ClientIPAddress { get; set; }

        /// <summary>
        /// Gets or sets the client's user agent string.
        /// </summary>
        string ClientUserAgent { get; set; }

        /// <summary>
        /// Gets or sets the stream that the client is reading from.
        /// </summary>
        //[IgnoreMember]
        //[JsonIgnore]
        //IClientReadStream? ClientStream { get; set; }

        /// <summary>
        /// Gets the HTTP response associated with the client.
        /// </summary>
        HttpResponse Response { get; }


        Pipe Pipe { get; set; }

        /// <summary>
        /// Gets the unique request identifier for the client.
        /// </summary>
        string UniqueRequestId { get; }

        /// <summary>
        /// Sets the unique request identifier for the client.
        /// </summary>
        /// <param name="uniqueRequestId">The unique request identifier to set.</param>
        void SetUniqueRequestId(string uniqueRequestId);
        void Stop();
    }
}
