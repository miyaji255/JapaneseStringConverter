using System;
using JapaneseStringConverter.Internal;

#if NETCOREAPP1_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace JapaneseStringConverter
{
    /// <summary>
    /// 文字列を変換する関数を提供します
    /// </summary>
    public static class StringUtility
    {
        /// <summary>
        /// stack に置く最大のbyte数
        /// </summary>
        internal const int MaxByteCount = 1024;
        /// <summary>
        /// charは2byteのため <see cref="MaxByteCount"/> の 1/2
        /// </summary>
        internal const int MaxCharCount = MaxByteCount / 2;

        /// <summary>
        /// ConvertTargets でしたものに文字列を変換します
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
#if NETCOREAPP1_0_OR_GREATER
        [return: NotNullIfNotNull("source")]
#endif
        public static string? Convert(string? source, ConvertTargets targets)
        {
            if (source is null)
                return null;

            if (targets == ConvertTargets.None)
                return source;

            if (InvalidTarget(targets))
                throw new ArgumentException($"{nameof(targets)}は有効な値ではありません");

            var sourceSpan = source.AsSpan();
            if (targets.HasFlag(ConvertTargets.Narrow))
            {
                var requiredLength = sourceSpan.Length * 2;
                Span<char> span = requiredLength > MaxCharCount
                    ? stackalloc char[requiredLength]
                    : new char[requiredLength];

                var sourceLength = sourceSpan.Length;
                if (targets.HasFlag(ConvertTargets.Uppercase))
                {
                    // 半角化でもとのspanに書き込む都合上後ろに入れる
                    sourceSpan.ToUpperInvariant(span.Slice(sourceLength, sourceLength));
                }
                else if (targets.HasFlag(ConvertTargets.Lowercase))
                {
                    sourceSpan.ToLowerInvariant(span.Slice(sourceLength, sourceLength));
                }
                else
                {
                    sourceSpan.CopyTo(span.Slice(sourceLength, sourceLength));
                }


                if (targets.HasFlag(ConvertTargets.Katakana))
                {
                    // 半角化よりカタカナ化のほうが優先
                    var length = Utf16StringConverter.ToKatakana(span.Slice(sourceLength, sourceLength), span.Slice(sourceLength, sourceLength));
                    length = Utf16StringConverter.ToNarrow(span.Slice(sourceLength, length), span);

                    return span.Slice(0, length).ToString();
                }
                else if (targets.HasFlag(ConvertTargets.Hiragana))
                {
                    // 半角化よりひらがな化のほうが優先
                    // spanは最初から2倍分だから ensured=true
                    var length = Utf16StringConverter.ToHiragana(span.Slice(0, sourceLength), ref span, true);
                    // カタカナはすべてひらがなに変換したはずなので長さが2倍になるものはないはず
                    length = Utf16StringConverter.ToNarrow(span.Slice(0, length), span);
                    return span.Slice(0, length).ToString();
                }
                else
                {
                    var length = Utf16StringConverter.ToNarrow(span.Slice(sourceLength, sourceLength), span);
                    return span.Slice(0, length).ToString();
                }
            }
            else
            {
                Span<char> span = sourceSpan.Length > MaxCharCount
                    ? stackalloc char[sourceSpan.Length]
                    : new char[sourceSpan.Length];

                if (targets.HasFlag(ConvertTargets.Uppercase))
                {
                    sourceSpan.ToUpperInvariant(span);
                }
                else if (targets.HasFlag(ConvertTargets.Lowercase))
                {
                    sourceSpan.ToLowerInvariant(span);
                }
                else
                {
                    sourceSpan.CopyTo(span);
                }


                if (targets.HasFlag(ConvertTargets.Katakana))
                {
                    // 全角化よりカタカナ化のほうが優先
                    var length = Utf16StringConverter.ToKatakana(span, span);
                    if (targets.HasFlag(ConvertTargets.Wide))
                    {
                        length = Utf16StringConverter.ToWide(span.Slice(0, length), span);
                    }

                    return span.Slice(0, length).ToString();
                }
                else if (targets.HasFlag(ConvertTargets.Hiragana))
                {
                    if (targets.HasFlag(ConvertTargets.Wide))
                    {
                        // ｱ => ア => あ のようなパターンに対応するため先に全角化
                        var length = Utf16StringConverter.ToWide(span, span);
                        length = Utf16StringConverter.ToHiragana(span.Slice(0, length), ref span, false);
                        return span.Slice(0, length).ToString();
                    }
                    else
                    {
                        var length = Utf16StringConverter.ToHiragana(span, ref span, false);
                        return span.Slice(0, length).ToString();
                    }
                }
                else if (targets.HasFlag(ConvertTargets.Wide))
                {
                    var length = Utf16StringConverter.ToWide(span, span);
                    return span.Slice(0, length).ToString();
                }
                else
                {
                    return span.ToString();
                }
            }
        }

        private static bool InvalidTarget(ConvertTargets type)
        {
            return (type & (ConvertTargets.Uppercase | ConvertTargets.Lowercase)) == (ConvertTargets.Uppercase | ConvertTargets.Lowercase)
                || (type & (ConvertTargets.Narrow | ConvertTargets.Wide)) == (ConvertTargets.Narrow | ConvertTargets.Wide)
                || (type & (ConvertTargets.Katakana | ConvertTargets.Hiragana)) == (ConvertTargets.Katakana | ConvertTargets.Hiragana);
        }
    }
}
