using System.Text;
using BenchmarkDotNet.Running;
using ConsoleApp;
using Microsoft.VisualBasic;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var narrow = string.Concat(Enumerable.Range('!', '~' - '!' + 1).Select(i => (char)i));
var wide = string.Concat(Enumerable.Range('！', '～' - '！' + 1).Select(i => (char)i));

Console.WriteLine(narrow);
Console.WriteLine(wide);
narrow = "クケコサシスセソタチツテト";
Console.WriteLine(narrow);
narrow = Strings.StrConv(narrow, VbStrConv.Hiragana | VbStrConv.Narrow);
Console.WriteLine(narrow);
#if !DEBUG
BenchmarkRunner.Run<BenchmarkTargets>();
#endif
Console.ReadKey();

