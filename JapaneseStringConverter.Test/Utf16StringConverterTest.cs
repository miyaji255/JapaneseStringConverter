using JapaneseStringConverter.Internal;

namespace JapaneseStringConverter.Test
{
    public class Utf16StringConverterTest
    {
        [Theory]
        [MemberData(nameof(WideNarrowSource))]
        [MemberData(nameof(NarrowSource))]
        public void ToNarrow(string exptected, string input)
        {
            var result = new char[input.Length * 2].AsSpan();

            var length = Utf16StringConverter.ToNarrow(input.AsSpan(), result);

            Assert.Equal(exptected, result.Slice(0, length).ToString());
        }

        [Theory]
        [MemberData(nameof(WideNarrowSource))]
        [MemberData(nameof(NarrowSource))]
        public void ToNarrowFromEnd(string expected, string input)
        {
            var result = new char[input.Length * 2].AsSpan();

            var startIndex = Utf16StringConverter.ToNarrowFromEnd(input.AsSpan(), result);

            Assert.Equal(expected, result.Slice(startIndex).ToString());
        }

        [Theory]
        [MemberData(nameof(WideNarrowSource))]
        [MemberData(nameof(WideSource))]
        public void ToWide(string input, string expected)
        {
            var result = new char[input.Length].AsSpan();

            var length = Utf16StringConverter.ToWide(input.AsSpan(), result);

            Assert.Equal(expected, result.Slice(0, length).ToString());
        }

        [Theory]
        [MemberData(nameof(WideNarrowSource))]
        [MemberData(nameof(WideSource))]
        public void ToWideFromEnd(string input, string expected)
        {
            var result = new char[input.Length].AsSpan();

            var startIndex = Utf16StringConverter.ToWideFromEnd(input.AsSpan(), result);

            Assert.Equal(expected, result.Slice(startIndex).ToString());
        }

        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
        [MemberData(nameof(HiraganaSource))]
        public void ToHiragana(string input, string expected)
        {
            var result = new char[input.Length].AsSpan();

            var length = Utf16StringConverter.ToHiragana(input.AsSpan(), ref result, false);

            Assert.Equal(expected, result.Slice(0, length).ToString());
        }

        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
        [MemberData(nameof(HiraganaSource))]
        public void ToHiraganaFromEnd(string input, string expected)
        {
            var result = new char[input.Length * 2].AsSpan();

            var startIndex = Utf16StringConverter.ToHiraganaFromEnd(input.AsSpan(), result);

            Assert.Equal(expected, result.Slice(startIndex).ToString());
        }

        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
        [MemberData(nameof(KatakanaSource))]
        public void ToKatakana(string expected, string input)
        {
            var result = new char[input.Length].AsSpan();

            var length = Utf16StringConverter.ToKatakana(input.AsSpan(), result);

            Assert.Equal(expected, result.Slice(0, length).ToString());
        }

        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
        [MemberData(nameof(KatakanaSource))]
        public void ToKatakanaFromEnd(string expected, string input)
        {
            var result = new char[input.Length * 2].AsSpan();

            var startIndex = Utf16StringConverter.ToKatakanaFromEnd(input.AsSpan(), result);

            Assert.Equal(expected, result.Slice(startIndex).ToString());
        }


        public static readonly string[][] WideNarrowSource = TestSources.WideNarrowTranslate;

        public static readonly string[][] WideSource = TestSources.Wide;

        public static readonly string[][] NarrowSource = TestSources.Narrow;

        public static readonly string[][] KanaTransrateSource = TestSources.KanaTransrate;

        public static readonly string[][] HiraganaSource = TestSources.Hiragana;

        public static readonly string[][] KatakanaSource = TestSources.Katakana;
    }
}
