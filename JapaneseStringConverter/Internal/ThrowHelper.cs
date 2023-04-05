using System;

#if !NET472
using System.Diagnostics.CodeAnalysis;
#endif

namespace JapaneseStringConverter.Internal
{
    internal static class ThrowHelper
    {
#if !NET472
        [DoesNotReturn]
#endif
        internal static void ThrowInvalidOperationException(string? message) {
            throw new InvalidOperationException(message);
        }
    }
}
