using System;

#if NETCOREAPP1_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace JapaneseStringConverter.Internal
{
    internal static class ThrowHelper
    {
#if NETCOREAPP1_0_OR_GREATER
        [DoesNotReturn]
#endif
        internal static void ThrowInvalidOperationException(string? message) {
            throw new InvalidOperationException(message);
        }
    }
}
