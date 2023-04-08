using System;

#if !NETFRAMEWORK
using System.Diagnostics.CodeAnalysis;
#endif

namespace JapaneseStringConverter.Internal
{
    internal static class ThrowHelper
    {
#if !NETFRAMEWORK
        [DoesNotReturn]
#endif
        internal static void ThrowInvalidOperationException(string? message) {
            throw new InvalidOperationException(message);
        }
    }
}
