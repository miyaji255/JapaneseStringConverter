using System.Text;
using BenchmarkDotNet.Attributes;
using CSharp.Japanese.Kanaxs;
using JapaneseStringConverter;
using Microsoft.VisualBasic;
#pragma warning disable CA1822

namespace ConsoleApp
{
    [MarkdownExporter]
    [MemoryDiagnoser]
    public class BenchmarkTargets
    {
        static readonly string _wide = "０１２３４５６７８９ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲンァィゥェォャュョッーヮヰヱヵヶヴガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポ！＃＄％＆＇（）＊＋，－．／：；＜＝＞？＠［］＾＿｀｛｜｝～　、。・「」゛￣￥“”　";
        static readonly string _narrow = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｬｭｮｯｰﾜｲｴｶｹｳﾞｶﾞｷﾞｸﾞｹﾞｺﾞｻﾞｼﾞｽﾞｾﾞｿﾞﾀﾞﾁﾞﾂﾞﾃﾞﾄﾞﾊﾞﾋﾞﾌﾞﾍﾞﾎﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ!#$%&'()*+,-./:;<=>?@[]^_`{|}~ ､｡･｢｣ﾞ~\\\"\" ";
        static readonly string _katakana = "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨララ゚リリ゚ルル゚レレ゚ロロ゚ヮワヷヰヸヱヹヲヺンヴヵヶヽヾ";
        static readonly string _hiragana = "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらら゚りり゚るる゚れれ゚ろろ゚ゎわわ゙ゐゐ゙ゑゑ゙をを゙んゔゕゖゝゞ";

        public BenchmarkTargets()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        #region Narrow
        [Benchmark]
        public void JapaneseStringConverterNarrow() => _wide.ToNarrow();

        [Benchmark]
#pragma warning disable CA1416 // Validate platform compatibility
        public void StrConvNarrow() => Strings.StrConv(_wide, VbStrConv.Narrow);
#pragma warning restore CA1416 // Validate platform compatibility

        [Benchmark]
        public void KanaxsNarrow() => KanaEx.ToHankaku(_wide);

        [Benchmark]
        public void KanaxsNarrowKana() => KanaEx.ToHankakuKana(_wide);

        [Benchmark]
        public void KanaxsNarrowBoth() => KanaEx.ToHankaku(KanaEx.ToHankakuKana(_wide));
        #endregion

        #region Wide
        [Benchmark]
        public void JapaneseStringConverterWide() => _narrow.ToWide();

        [Benchmark]
#pragma warning disable CA1416 // Validate platform compatibility
        public void StrConvWide() => Strings.StrConv(_narrow, VbStrConv.Wide);
#pragma warning restore CA1416 // Validate platform compatibility

        [Benchmark]
        public void KanaxsWide() => KanaEx.ToZenkaku(_narrow);

        [Benchmark]
        public void KanaxsWideKana() => KanaEx.ToZenkakuKana(_narrow);

        [Benchmark]
        public void KanaxsWideBoth() => KanaEx.ToZenkaku(KanaEx.ToZenkakuKana(_narrow));
        #endregion

        #region Hiragana
        [Benchmark]
        public void JapaneseStringConverterHiragana() => _katakana.ToHiragana();

        [Benchmark]
#pragma warning disable CA1416 // Validate platform compatibility
        public void StrConvHiragana() => Strings.StrConv(_katakana, VbStrConv.Hiragana);
#pragma warning restore CA1416 // Validate platform compatibility

        [Benchmark]
        public void KanaxsHiragana() => KanaEx.ToHiragana(_katakana);
        #endregion

        #region Katakana
        [Benchmark]
        public void JapaneseStringConverterKatakana() => _hiragana.ToKatakana();

        [Benchmark]
#pragma warning disable CA1416 // Validate platform compatibility
        public void StrConvKatakana() => Strings.StrConv(_hiragana, VbStrConv.Katakana);
#pragma warning restore CA1416 // Validate platform compatibility

        [Benchmark]
        public void KanaxsKatakana() => KanaEx.ToKatakana(_hiragana);
        #endregion
    }
}
