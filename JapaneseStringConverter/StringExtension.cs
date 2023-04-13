using System;
using System.Runtime.CompilerServices;
using JapaneseStringConverter.Internal;

#if NETCOREAPP1_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace JapaneseStringConverter
{
    /// <summary>
    /// 文字列を変換する拡張メソッドを提供します
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 変換のために十分な長さを返します
        /// </summary>
        public static int GetSafeConvertLength(this string source, ConvertTargets type)
        {
            return (type & (ConvertTargets.Narrow | ConvertTargets.Hiragana)) != ConvertTargets.None ? source.Length << 1 : source.Length;
        }

        #region Narrow
        /// <summary>
        /// 全角文字を半角文字に変換します
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP1_0_OR_GREATER
        [return: NotNullIfNotNull("source")]
#endif
        public static string? ToNarrow(this string? source)
        {
            if (source is null)
                return null;
            var sourceSpan = source.AsSpan();

            var requiredLength = sourceSpan.Length * 2;
            Span<char> result = requiredLength <= StringUtility.MaxCharCount
                ? stackalloc char[requiredLength]
                : new char[requiredLength];

            var startIndex = Utf16StringConverter.ToNarrowFromEnd(source.AsSpan(), result);

            return result.Slice(startIndex).ToString();
        }

        /// <summary>
        /// 全角文字を半角文字に変換します
        /// </summary>
        /// <returns> destination に書き込まれた文字数です。destination が小さかった場合は-1です。</returns>
        /// <exception cref="InvalidOperationException">source と destination が重なっているときに発生します</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToNarrow(this ReadOnlySpan<char> source, Span<char> destination)
        {
            if (source.Overlaps(destination))
                ThrowHelper.ThrowInvalidOperationException("This operation is invalid on overlapping buffers.");

            return Utf16StringConverter.ToNarrow(source, destination);
        }
        #endregion

        #region Wide
        /// <summary>
        /// 半角文字を全角文字に変換します
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP1_0_OR_GREATER
        [return: NotNullIfNotNull("source")]
#endif
        public static string? ToWide(this string? source)
        {
            if (source is null)
                return null;

            var sourceSpan = source.AsSpan();

            Span<char> result = sourceSpan.Length <= StringUtility.MaxCharCount
                ? stackalloc char[sourceSpan.Length]
                : new char[sourceSpan.Length];

            var startIndex = Utf16StringConverter.ToWideFromEnd(source.AsSpan(), result);

            return result.Slice(startIndex).ToString();
        }

        /// <summary>
        /// 半角文字を全角文字に変換します
        /// </summary>
        /// <returns> destination に書き込まれた文字数です。destination が小さかった場合は-1です。</returns>
        /// <exception cref="InvalidOperationException">source と destination が重なっているときに発生します</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToWide(this ReadOnlySpan<char> source, Span<char> destination)
        {
            if (source.Overlaps(destination))
                ThrowHelper.ThrowInvalidOperationException("This operation is invalid on overlapping buffers.");

            return Utf16StringConverter.ToWide(source, destination);
        }

        /// <summary>
        /// 半角文字を全角文字に上書きします
        /// </summary>
        /// <returns> source に書き込まれた文字数です。source が小さかった場合は-1です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToWideOverride(Span<char> source)
        {
            return Utf16StringConverter.ToWide(source, source);
        }
        #endregion

        #region Hiragana
        /// <summary>
        /// 全角カタカナをひらがなに変換します
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP1_0_OR_GREATER
        [return: NotNullIfNotNull("source")]
#endif
        public static string? ToHiragana(this string? source)
        {
            if (source is null)
                return null;

            var sourceSpan = source.AsSpan();

            var requiredLength = sourceSpan.Length * 2;
            Span<char> result = requiredLength <= StringUtility.MaxCharCount
                ? stackalloc char[requiredLength]
                : new char[requiredLength];

            var startIndex = Utf16StringConverter.ToHiraganaFromEnd(source.AsSpan(), result);

            return result.Slice(startIndex).ToString();
        }

        /// <summary>
        /// 全角カタカナをひらがなに変換します
        /// </summary>
        /// <returns> destination に書き込まれた文字数です。destination が小さかった場合は-1です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToHiragana(this ReadOnlySpan<char> source, Span<char> destination)
        {
            if (source.Overlaps(destination))
                ThrowHelper.ThrowInvalidOperationException("This operation is invalid on overlapping buffers.");

            return Utf16StringConverter.ToHiragana(source, ref destination, true);
        }
        #endregion

        #region Katakana
        /// <summary>
        /// ひらがなを全角カタカナに上書きします
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP1_0_OR_GREATER
        [return: NotNullIfNotNull("source")]
#endif
        public static string? ToKatakana(this string? source)
        {
            if (source is null)
                return null;

            var sourceSpan = source.AsSpan();

            Span<char> result = sourceSpan.Length <= StringUtility.MaxCharCount
                ? stackalloc char[sourceSpan.Length]
                : new char[sourceSpan.Length];

            var startIndex = Utf16StringConverter.ToKatakanaFromEnd(source.AsSpan(), result);

            return result.Slice(startIndex).ToString();
        }

        /// <summary>
        /// ひらがなを全角カタカナに上書きします
        /// </summary>
        /// <returns> destination に書き込まれた文字数です。destination が小さかった場合は-1です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToKatakana(this ReadOnlySpan<char> source, Span<char> destination)
        {
            if (source.Overlaps(destination))
                ThrowHelper.ThrowInvalidOperationException("This operation is invalid on overlapping buffers.");

            return Utf16StringConverter.ToKatakana(source, destination);
        }
        #endregion
    }
}
