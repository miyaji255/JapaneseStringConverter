namespace JapaneseStringConverter.Test
{
    public class ToWideNarrowTest
    {
        [Theory]
        [MemberData(nameof(WideNarrowSource))]
        [MemberData(nameof(WideSource))]
        public void ToWide(string input, string expected)
        {
            var result = input.ToWide();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToWideException()
        {
            var result = StringExtensions.ToWide(null);
            Assert.Null(result);

            var temp = new char[] { 't', 'e', 's', 't' };
            var exception = Record.Exception(() => StringExtensions.ToWide(temp, temp));
            Assert.IsType<InvalidOperationException>(exception);

            var length = StringExtensions.ToWide("test".AsSpan(), stackalloc char[2]);
            Assert.Equal(-1, length);
        }

        [Theory]
        [MemberData(nameof(WideNarrowSource))]
        [MemberData(nameof(WideSource))]
        public void StringUtilWide(string input, string expected)
        {
            var result = StringUtility.Convert(input, ConvertTargets.Wide);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(WideNarrowSource))]
        [MemberData(nameof(NarrowSource))]
        public void ToNarrow(string expected, string input)
        {
            var result = input.ToNarrow();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToNarrowException()
        {
            var result = StringExtensions.ToNarrow(null);
            Assert.Null(result);

            var temp = new char[] { 'ｔ', 'ｅ', 'ｓ', 'ｔ' };
            var exception = Record.Exception(() => StringExtensions.ToNarrow(temp, temp));
            Assert.IsType<InvalidOperationException>(exception);

            var length = StringExtensions.ToNarrow("ｔｅｓｔ".AsSpan(), stackalloc char[2]);
            Assert.Equal(-1, length);
        }

        [Theory]
        [MemberData(nameof(WideNarrowSource))]
        [MemberData(nameof(NarrowSource))]
        public void StringUtilNarrow(string expected, string input)
        {
            var result = StringUtility.Convert(input, ConvertTargets.Narrow);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(" !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~")]
        [InlineData("ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾗﾟﾘﾘﾟﾙﾙﾟﾚﾚﾟﾛﾛﾟヮﾜﾜﾞヰヸヱヹｦｦﾞﾝｳﾞヵヶヽヾ")]
        [InlineData("ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらりるれろゎわゐゑをん")]
        public void WideNarrow(string input)
        {
            var result = input.ToWide().ToNarrow();

            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData("　！＂＃＄％＆＇（）＊＋，－．／０１２３４５６７８９：；＜＝＞？＠ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ［￥］＾＿｀ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ｛｜｝～")]
        [InlineData("ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨララ゚リリ゚ルル゚レレ゚ロロ゚ヮワヷヰヸヱヹヲヺンヴヵヶヽヾ")]
        [InlineData("ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらりるれろゎわゐゑをん")]
        public void NarrowWide(string input)
        {
            var result = input.ToNarrow().ToWide();

            Assert.Equal(input, result);
        }

        public static readonly string[][] WideNarrowSource = TestSources.WideNarrowTranslate;

        public static readonly string[][] WideSource = TestSources.Wide;

        public static readonly string[][] NarrowSource = TestSources.Narrow;
    }
}
