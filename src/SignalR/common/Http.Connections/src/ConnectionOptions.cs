// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.AspNetCore.Http.Connections
{
    /// <summary>
    /// Options used to change behavior of how connections are handled.
    /// </summary>
    public class ConnectionOptions
    {
        /// <summary>
        /// Gets or sets the interval used by the server to timeout idle connections.
        /// </summary>
        public TimeSpan? DisconnectTimeout { get; set; }

        /// <summary>
        /// Gets or sets the delay after shutdown is triggered before connections are closed.
        /// </summary>
        /// <remarks>
        /// The Server or Host may forcefully close connections before this delay completes.
        /// </remarks>
        public TimeSpan? ShutdownDelay { get; set; }
    }
}
