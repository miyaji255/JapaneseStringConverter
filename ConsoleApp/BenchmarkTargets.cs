using System.Text;
using BenchmarkDotNet.Attributes;
using JapaneseStringConverter;
using Microsoft.VisualBasic;
using CSharp.Japanese.Kanaxs;
#pragma warning disable CA1822

namespace ConsoleApp
{
    [MarkdownExporter]
    [MemoryDiagnoser]
    public class BenchmarkTargets
    {
        static readonly string _wide = "０１２３４５６７８９ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲンァィゥェォャュョッーヮヰヱヵヶヴガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポ！＃＄％＆＇（）＊＋，－．／：；＜＝＞？＠［］＾＿｀｛｜｝～　、。・「」゛￣￥“”　";
        static readonly string _narrow = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｬｭｮｯｰﾜｲｴｶｹｳﾞｶﾞｷﾞｸﾞｹﾞｺﾞｻﾞｼﾞｽﾞｾﾞｿﾞﾀﾞﾁﾞﾂﾞﾃﾞﾄﾞﾊﾞﾋﾞﾌﾞﾍﾞﾎﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ!#$%&'()*+,-./:;<=>?@[]^_`{|}~ ､｡･｢｣ﾞ~\\\"\" ";

        public BenchmarkTargets()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Benchmark]
        public string JapaneseStringConverterToNarrow() => _wide.ToNarrow();

        [Benchmark]
#pragma warning disable CA1416 // Validate platform compatibility
        public string? StrConvToNarrow() => Strings.StrConv(_wide, VbStrConv.Narrow);
#pragma warning restore CA1416 // Validate platform compatibility

        [Benchmark]
        public string KanaxsNarrow() => KanaEx.ToHankaku(_wide);

        [Benchmark]
        public string KanaxsNarrowKana() => KanaEx.ToHankakuKana(_wide);

        [Benchmark]
        public string KanaxsNarrowBoth() => KanaEx.ToHankaku(KanaEx.ToHankakuKana(_wide));

        [Benchmark]
        public string JapaneseStringConverterToWide() => _narrow.ToWide();

        [Benchmark]
#pragma warning disable CA1416 // Validate platform compatibility
        public string? StrConvToWide()=> Strings.StrConv(_narrow, VbStrConv.Wide);
#pragma warning restore CA1416 // Validate platform compatibility

        [Benchmark]
        public string KanaxsWide() => KanaEx.ToZenkaku(_narrow);

        [Benchmark]
        public string KanaxsWideKana() => KanaEx.ToZenkakuKana(_narrow);

        [Benchmark]
        public string KanaxsWideBoth() => KanaEx.ToZenkaku(KanaEx.ToZenkakuKana(_narrow));
    }
}
