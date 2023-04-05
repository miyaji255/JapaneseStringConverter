namespace JapaneseStringConverter.Test
{
    public class StringUtilityTest
    {
        [Theory]
        [InlineData("ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ", ConvertTargets.Lowercase | ConvertTargets.Narrow, "abcdefghijklmnopqrstuvwxyz")]
        [InlineData(
            "ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾗﾟﾘﾘﾟﾙﾙﾟﾚﾚﾟﾛﾛﾟヮﾜﾜﾞヰヸヱヹｦｦﾞﾝｳﾞヵヶヽヾabcdefghijklmnopqrstuvwxyz",
            ConvertTargets.Wide | ConvertTargets.Hiragana | ConvertTargets.Uppercase,
            "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらら゚りり゚るる゚れれ゚ろろ゚ゎわわ゙ゐゐ゙ゑゑ゙をを゙んゔゕゖゝゞＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ")]
        [InlineData(
            "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらら゚りり゚るる゚れれ゚ろろ゚ゎわわ゙ゐゐ゙ゑゑ゙をを゙んゔゕゖゝゞＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ",
            ConvertTargets.Narrow | ConvertTargets.Katakana | ConvertTargets.Lowercase,
            "ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾗﾟﾘﾘﾟﾙﾙﾟﾚﾚﾟﾛﾛﾟヮﾜﾜﾞヰヸヱヹｦｦﾞﾝｳﾞヵヶヽヾabcdefghijklmnopqrstuvwxyz")]
        public void Convert(string input, ConvertTargets convertType, string expected)
        {
            var result = StringUtility.Convert(input, convertType);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertException()
        {
            var result = StringUtility.Convert(null, ConvertTargets.Lowercase);
            Assert.Null(result);

            var exception = Record.Exception(() => StringUtility.Convert("a", ConvertTargets.Lowercase | ConvertTargets.Uppercase));
            Assert.IsType<ArgumentException>(exception);

            exception = Record.Exception(() => StringUtility.Convert("a", ConvertTargets.Narrow | ConvertTargets.Wide));
            Assert.IsType<ArgumentException>(exception);

            exception = Record.Exception(() => StringUtility.Convert("a", ConvertTargets.Hiragana | ConvertTargets.Katakana));
            Assert.IsType<ArgumentException>(exception);
        }
    }
}
