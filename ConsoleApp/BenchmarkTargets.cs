using System.Text;
using BenchmarkDotNet.Attributes;
using JapaneseStringConverter;
using Microsoft.VisualBasic;
#pragma warning disable CA1822

namespace ConsoleApp
{
    [MarkdownExporter]
    [MemoryDiagnoser]
    public class BenchmarkTargets
    {
        const string wide = "０１２３４５６７８９ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲンァィゥェォャュョッーヮヰヱヵヶヴガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポ！＃＄％＆＇（）＊＋，－．／：；＜＝＞？＠［］＾＿｀｛｜｝～　、。・「」゛￣￥“”　";
        const string narrow = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝｧｨｩｪｫｬｭｮｯｰﾜｲｴｶｹｳﾞｶﾞｷﾞｸﾞｹﾞｺﾞｻﾞｼﾞｽﾞｾﾞｿﾞﾀﾞﾁﾞﾂﾞﾃﾞﾄﾞﾊﾞﾋﾞﾌﾞﾍﾞﾎﾞﾊﾟﾋﾟﾌﾟﾍﾟﾎﾟ!#$%&'()*+,-./:;<=>?@[]^_`{|}~ ､｡･｢｣ﾞ~\\\"\" ";

        public BenchmarkTargets() {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Benchmark]
        public string ToNarrow() {
            return wide.ToNarrow();
        }

        [Benchmark]
        public string? StrConvToNarrow() {
#pragma warning disable CA1416 // Validate platform compatibility
            return Strings.StrConv(wide, VbStrConv.Narrow);
#pragma warning restore CA1416 // Validate platform compatibility
        }

        [Benchmark]
        public string ToWide() {
            return narrow.ToWide();
        }

        [Benchmark]
        public string? StrConvToWide() {
#pragma warning disable CA1416 // Validate platform compatibility
            return Strings.StrConv(narrow, VbStrConv.Wide);
#pragma warning restore CA1416 // Validate platform compatibility
        }
    }
}
