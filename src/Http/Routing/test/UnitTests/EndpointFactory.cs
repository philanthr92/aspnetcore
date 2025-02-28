﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing.TestObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Routing
{
    internal static class EndpointFactory
    {
        public static RouteEndpoint CreateRouteEndpoint(
            string template,
            object defaults = null,
            object policies = null,
            object requiredValues = null,
            int order = 0,
            string displayName = null,
            params object[] metadata)
        {
            var routePattern = RoutePatternFactory.Parse(template, defaults, policies, requiredValues);

            return CreateRouteEndpoint(routePattern, order, displayName, metadata);
        }

        public static RouteEndpoint CreateRouteEndpoint(
            RoutePattern routePattern = null,
            int order = 0,
            string displayName = null,
            IList<object> metadata = null)
        {
            return new RouteEndpoint(
                TestConstants.EmptyRequestDelegate,
                routePattern,
                order,
                new EndpointMetadataCollection(metadata ?? Array.Empty<object>()),
                displayName);
        }
    }
}
