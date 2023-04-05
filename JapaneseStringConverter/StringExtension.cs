#pragma warning disable IDE0057
using System;
using System.Runtime.CompilerServices;
using JapaneseStringConverter.Internal;

#if !NET472
using System.Diagnostics.CodeAnalysis;
#endif

namespace JapaneseStringConverter
{
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
#if !NET472
        [return: NotNullIfNotNull("source")]
#endif
        public static string? ToNarrow(this string? source)
        {
            if (source is null)
                return null;
            var sourceSpan = source.AsSpan();

            Span<char> result = sourceSpan.Length <= StringUtility.MaxNarrowCharLimit
                ? stackalloc char[sourceSpan.Length * 2]
                : new char[sourceSpan.Length * 2];

            var length = UnicodeStringConverter.ToNarrow(source.AsSpan(), result);

            return result.Slice(0, length).ToString();
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

            return UnicodeStringConverter.ToNarrow(source, destination);
        }
        #endregion

        #region Wide
        /// <summary>
        /// 半角文字を全角文字に変換します
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !NET472
        [return: NotNullIfNotNull("source")]
#endif
        public static string? ToWide(this string? source)
        {
            if (source is null)
                return null;

            var sourceSpan = source.AsSpan();

            Span<char> result = sourceSpan.Length <= StringUtility.MaxWideCharLimit
                ? stackalloc char[sourceSpan.Length]
                : new char[sourceSpan.Length];

            var length = UnicodeStringConverter.ToWide(source.AsSpan(), result);

            return result.Slice(0, length).ToString();
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

            return UnicodeStringConverter.ToWide(source, destination);
        }

        /// <summary>
        /// 半角文字を全角文字に上書きします
        /// </summary>
        /// <returns> source に書き込まれた文字数です。source が小さかった場合は-1です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToWideOverride(Span<char> source)
        {
            return UnicodeStringConverter.ToWide(source, source);
        }
        #endregion

        #region Hiragana
        /// <summary>
        /// 全角カタカナをひらがなに変換します
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !NET472
        [return: NotNullIfNotNull("source")]
#endif
        public static string? ToHiragana(this string? source)
        {
            if (source is null)
                return null;

            var sourceSpan = source.AsSpan();

            Span<char> result = sourceSpan.Length <= StringUtility.MaxWideCharLimit
                ? stackalloc char[sourceSpan.Length]
                : new char[sourceSpan.Length];

            var length = UnicodeStringConverter.ToHiragana(source.AsSpan(), ref result, false);

            return result.Slice(0, length).ToString();
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

            return UnicodeStringConverter.ToHiragana(source, ref destination, true);
        }
        #endregion

        #region Katakana
        /// <summary>
        /// ひらがなを全角カタカナに上書きします
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !NET472
        [return: NotNullIfNotNull("source")]
#endif
        public static string? ToKatakana(this string? source)
        {
            if (source is null)
                return null;

            var sourceSpan = source.AsSpan();

            Span<char> result = sourceSpan.Length <= StringUtility.MaxWideCharLimit
                ? stackalloc char[sourceSpan.Length]
                : new char[sourceSpan.Length];

            var length = UnicodeStringConverter.ToKatakana(source.AsSpan(), result);

            return result.Slice(0, length).ToString();
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

            return UnicodeStringConverter.ToKatakana(source, destination);
        }
        #endregion
    }
}