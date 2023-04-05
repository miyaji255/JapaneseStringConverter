namespace JapaneseStringConverter.Test
{
    public class ToKanaTransrateTest
    {
        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
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
        public void StringUtilHiragana(string input, string expected)
        {
            var result = StringUtility.Convert(input, ConvertTargets.Hiragana);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(KanaTransrateSource))]
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
        public void StringUtilKatakana(string expected, string input)
        {
            var result = StringUtility.Convert(input, ConvertTargets.Katakana);

            Assert.Equal(expected, result);
        }

        public static readonly string[][] KanaTransrateSource = new string[][] {
            new[] { "アイウエオ", "あいうえお" },
            new[] { "ガギグゲゴ", "がぎぐげご" },
            new[] { "ＡＢＣ", "ＡＢＣ" },
            new[]
            {
                " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~\r\n",
                " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~\r\n"
            },
            new[]
            {
                "　！＂＃＄％＆＇（）＊＋，－．／０１２３４５６７８９：；＜＝＞？＠ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ［＼］＾＿｀ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ｛｜｝～",
                "　！＂＃＄％＆＇（）＊＋，－．／０１２３４５６７８９：；＜＝＞？＠ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ［＼］＾＿｀ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ｛｜｝～",
           },
            new[]
            {
                "ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾗﾟﾘﾘﾟﾙﾙﾟﾚﾚﾟﾛﾛﾟﾜﾜﾞｦｦﾞﾝｳﾞ",
                "ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾗﾟﾘﾘﾟﾙﾙﾟﾚﾚﾟﾛﾛﾟﾜﾜﾞｦｦﾞﾝｳﾞ"
            },
            new[]
            {
                "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨララ゚リリ゚ルル゚レレ゚ロロ゚ヮワヷヰヸヱヹヲヺンヴヵヶヽヾ" ,
                "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらら゚りり゚るる゚れれ゚ろろ゚ゎわわ゙ゐゐ゙ゑゑ゙をを゙んゔゕゖゝゞ"
            },
            new[] { "ラ゚ラ゚ラ゚ラ゚", "ら゚ら゚ら゚ら゚" },
            new[] { "ヷヸヹヺ", "わ゙ゐ゙ゑ゙を゙" }
        };

        public static readonly string[][] HiraganaSource = new string[][]
        {
            new[]
            {
                "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらら゚りり゚るる゚れれ゚ろろ゚ゎわわ゙ゐゐ゙ゑゑ゙をを゙んゔゕゖゝゞ",
                "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらら゚りり゚るる゚れれ゚ろろ゚ゎわわ゙ゐゐ゙ゑゑ゙をを゙んゔゕゖゝゞ"
            },
        };

        public static readonly string[][] KatakanaSource = new string[][]
        {
            new[]
            {
                "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨララ゚リリ゚ルル゚レレ゚ロロ゚ヮワヷヰヸヱヹヲヺンヴヵヶヽヾ" ,
                "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨララ゚リリ゚ルル゚レレ゚ロロ゚ヮワヷヰヸヱヹヲヺンヴヵヶヽヾ"
            },
        };
    }
}
