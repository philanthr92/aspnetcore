// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CodeGenerator
{
    public static class ReadOnlySpanStaticDataGenerator
    {
        public static string GenerateFile(string namespaceName, string className, IEnumerable<(string Name, string Value)> allProperties)
        {
            var properties = allProperties.Select((p, index) => new Property
            {
                Data = p,
                Index = index
            });

            var s = $@"// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

#nullable enable

namespace {namespaceName}
{{
    internal partial class {className}
    {{
        // This uses C# compiler's ability to refer to static data directly. For more information see https://vcsjones.dev/2019/02/01/csharp-readonly-span-bytes-static
     {Each(properties, p => $@"
        private static ReadOnlySpan<byte> {p.Data.Name}Bytes => new byte[{p.Data.Value.Length}] {{ {GetDataAsBytes(p.Data.Value)} }};")}
    }}
}}
";

            // Temporary workaround for https://github.com/dotnet/runtime/issues/55688
            return s.Replace("{{", "{").Replace("}}", "}");
        }

        private static string Each<T>(IEnumerable<T> values, Func<T, string> formatter)
        {
            return values.Any() ? values.Select(formatter).Aggregate((a, b) => a + b) : "";
        }

        private static string GetDataAsBytes(string value)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < value.Length; ++i)
            {
                var c = value[i];
                if (c == '\n')
                {
                    stringBuilder.Append("(byte)'\\n'");
                }
                else if (c == '\r')
                {
                    stringBuilder.Append("(byte)'\\r'");
                }
                else
                {
                    stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "(byte)'{0}'", c);
                }

                if (i < value.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

        private class Property
        {
            public (string Name, string Value) Data;
            public int Index;
        }
    }
}
