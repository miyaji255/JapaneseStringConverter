namespace JapaneseStringConverter.Test
{
    public class ToKanaTransrateTest
    {
        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
        [MemberData(nameof(HiraganaSource))]
        public void ToHiragana(string input, string expected)
        {
            var result = input.ToHiragana();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToHiraganaException()
        {
            var result = StringExtensions.ToHiragana(null);
            Assert.Null(result);

            var temp = new char[] { 'テ', 'ス', 'ト' };
            var exception = Record.Exception(() => StringExtensions.ToHiragana(temp, temp));
            Assert.IsType<InvalidOperationException>(exception);

            var length = StringExtensions.ToHiragana("テスト".AsSpan(), stackalloc char[2]);
            Assert.Equal(-1, length);
        }

        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
        [MemberData(nameof(HiraganaSource))]
        public void StringUtilHiragana(string input, string expected)
        {
            var result = StringUtility.Convert(input, ConvertTargets.Hiragana);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
        [MemberData(nameof(KatakanaSource))]
        public void ToKatakana(string expected, string input)
        {
            var result = input.ToKatakana();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToKatakanaException()
        {
            var result = StringExtensions.ToKatakana(null);
            Assert.Null(result);

            var temp = new char[] { 'て', 'す', 'と' };
            var exception = Record.Exception(() => StringExtensions.ToKatakana(temp, temp));
            Assert.IsType<InvalidOperationException>(exception);

            var length = StringExtensions.ToKatakana("てすと".AsSpan(), stackalloc char[2]);
            Assert.Equal(-1, length);
        }

        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
        [MemberData(nameof(KatakanaSource))]
        public void StringUtilKatakana(string expected, string input)
        {
            var result = StringUtility.Convert(input, ConvertTargets.Katakana);

            Assert.Equal(expected, result);
        }

        public static readonly string[][] KanaTransrateSource = TestSources.KanaTransrate;

        public static readonly string[][] HiraganaSource = TestSources.Hiragana;

        public static readonly string[][] KatakanaSource = TestSources.Katakana;
    }
}
