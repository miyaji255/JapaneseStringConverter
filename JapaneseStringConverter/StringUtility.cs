using System;
using JapaneseStringConverter.Internal;

#if !NETFRAMEWORK
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
        internal const int MaxByteLimit = 1024;
        /// <summary>
        /// charは2byteで半角時に文字数が最大で2倍になるため <see cref="MaxByteLimit"/> の1/4
        /// </summary>
        internal const int MaxNarrowCharLimit = MaxByteLimit / 4;
        /// <summary>
        /// charは2byteのため <see cref="MaxByteLimit"/> の 1/2
        /// </summary>
        internal const int MaxWideCharLimit = MaxByteLimit / 2;

        /// <summary>
        /// ConvertTargets でしたものに文字列を変換します
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
#if !NETFRAMEWORK
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
                Span<char> span = sourceSpan.Length > MaxNarrowCharLimit
                    ? stackalloc char[sourceSpan.Length * 2]
                    : new char[sourceSpan.Length * 2];

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
                    var length = UnicodeStringConverter.ToKatakana(span.Slice(sourceLength, sourceLength), span.Slice(sourceLength, sourceLength));
                    length = UnicodeStringConverter.ToNarrow(span.Slice(sourceLength, length), span);

                    return span.Slice(0, length).ToString();
                }
                else if (targets.HasFlag(ConvertTargets.Hiragana))
                {
                    // 半角化よりひらがな化のほうが優先
                    // spanは最初から2倍分だから ensured=true
                    var length = UnicodeStringConverter.ToHiragana(span.Slice(0, sourceLength), ref span, true);
                    // カタカナはすべてひらがなに変換したはずなので長さが2倍になるものはないはず
                    length = UnicodeStringConverter.ToNarrow(span.Slice(0, length), span);
                    return span.Slice(0, length).ToString();
                }
                else
                {
                    var length = UnicodeStringConverter.ToNarrow(span.Slice(sourceLength, sourceLength), span);
                    return span.Slice(0, length).ToString();
                }
            }
            else
            {
                Span<char> span = sourceSpan.Length > MaxWideCharLimit
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
                    var length = UnicodeStringConverter.ToKatakana(span, span);
                    if (targets.HasFlag(ConvertTargets.Wide))
                    {
                        length = UnicodeStringConverter.ToWide(span.Slice(0, length), span);
                    }

                    return span.Slice(0, length).ToString();
                }
                else if (targets.HasFlag(ConvertTargets.Hiragana))
                {
                    if (targets.HasFlag(ConvertTargets.Wide))
                    {
                        // ｱ => ア => あ のようなパターンに対応するため先に全角化
                        var length = UnicodeStringConverter.ToWide(span, span);
                        length = UnicodeStringConverter.ToHiragana(span.Slice(0, length), ref span, false);
                        return span.Slice(0, length).ToString();
                    }
                    else
                    {
                        var length = UnicodeStringConverter.ToHiragana(span, ref span, false);
                        return span.Slice(0, length).ToString();
                    }
                }
                else if (targets.HasFlag(ConvertTargets.Wide))
                {
                    var length = UnicodeStringConverter.ToWide(span, span);
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
