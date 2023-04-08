using System.Runtime.Serialization;
using System.Text;
using CommandLine;
using JapaneseStringConverter.Internal;

Console.OutputEncoding = Encoding.UTF8;

#if DEBUG
OutputNWUtility();
OutputHKUtility();
#else
BenchmarkDotNet.Running.BenchmarkRunner.Run<ConsoleApp.BenchmarkTargets>();
#endif

static void OutputNWUtility()
{
    var narrowChars = new StringBuilder(600);
    var wideChars = new StringBuilder(400);

    var markdownPair = new HashSet<(string, string)>();

    var span = (stackalloc char[2]);
    for (var i = (int)char.MinValue; i <= char.MaxValue; i++)
    {
        var c = (char)i;
        span[0] = c;
        span[1] = '\u0000';
        var length = UnicodeStringConverter.ToWide(span[0..1], span);
        if (span[1] != '\u0000' || span[0] != c)
        {
            if (c is 'a' or 'A' or '0' or 'ｦ' or 'ｧ' or 'ｱ' or 'ｶ' or 'ｻ' or 'ﾀ' or 'ﾅ' or 'ﾊ' or 'ﾏ' or 'ﾔ' or 'ﾗ' or 'ﾜ' or '￩')
                narrowChars.Append('\n');
            narrowChars.Append('\'');
            if (c is '\\' or '\'')
                narrowChars.Append('\\');
            narrowChars.Append(c);
            narrowChars.Append('\'');
            narrowChars.Append(',');
            narrowChars.Append(' ');
            if (c is 'z' or 'Z' or '9')
                narrowChars.Append('\n');

            markdownPair.Add((c.ToString(), span[..length].ToString()));
        }

        span[0] = '\u0000';
        span[1] = c;
        length = UnicodeStringConverter.ToNarrow(span[1..2], span);
        if (span[0] != c)
        {
            if (c is 'ァ' or 'カ' or 'サ' or 'タ' or 'ナ' or 'ハ' or 'マ' or 'ャ' or 'ラ' or 'ワ' or '！' or '０' or 'Ａ' or 'ａ')
                wideChars.Append('\n');
            wideChars.Append('\'');
            if (c is '\\' or '\'')
                wideChars.Append('\\');
            wideChars.Append(c);
            wideChars.Append('\'');
            wideChars.Append(',');
            wideChars.Append(' ');
            if (c is '○' or 'ヺ' or '９' or 'Ｚ' or 'ｚ')
                wideChars.Append('\n');

            markdownPair.Add((span[..length].ToString(), c.ToString()));
        }
    }

    Console.WriteLine("WideTargets");
    Console.WriteLine(narrowChars.ToString());
    Console.WriteLine("NarrowTargets");
    Console.WriteLine(wideChars.ToString());

    var builder = new StringBuilder(1000);
    var count = 0;
    builder.AppendLine("|半角|全角||半角|全角||半角|全角||半角|全角||半角|全角|");
    builder.AppendLine("|-|-|-| -|-|-| -|-|-| -|-|-| -|-|");
    foreach (var pair in markdownPair.OrderBy(p => p.Item1[0]))
    {
        if (count >= 5)
        {
            builder.Append('\n');
            count = 0;
        }
        builder.Append('|');
        if (pair.Item1 == " ")
        {
            builder.Append("U+0020|U+3000[^1]");
        }
        else
        {
            if (pair.Item1 is "\\" or "`" or "[" or "]")
                builder.Append('\\');
            builder.Append(pair.Item1);
            builder.Append('|');
            builder.Append(pair.Item2);
        }
        builder.Append('|');
        count++;
    }
    while (count <= 5)
    {
        builder.Append('|');
        builder.Append('|');
        builder.Append('|');
        count++;
    }
    Console.WriteLine(builder.ToString());
}

static void OutputHKUtility()
{

    var hiraganaChars = new StringBuilder(600);
    var katakanaChars = new StringBuilder(400);

    var markdownPair = new HashSet<(string, string)>();

    var span = (stackalloc char[2]);
    for (var i = (int)char.MinValue; i <= char.MaxValue; i++)
    {
        var c = (char)i;
        span[0] = c;
        span[1] = '\u0000';
        var length = UnicodeStringConverter.ToKatakana(span[0..1], span);
        if (span[1] != '\u0000' || span[0] != c)
        {
            if (c is 'か' or 'さ' or 'た' or 'な' or 'は' or 'ま' or 'ゃ' or 'ら' or 'ゎ' or 'ゔ')
                hiraganaChars.Append('\n');
            hiraganaChars.Append('\'');
            hiraganaChars.Append(c);
            hiraganaChars.Append('\'');
            hiraganaChars.Append(',');
            hiraganaChars.Append(' ');

            markdownPair.Add((c.ToString(), span[..length].ToString()));
        }

        span[0] = '\u0000';
        span[1] = c;
        length = UnicodeStringConverter.ToHiragana(span[1..2], ref span, true);
        if (span[0] != c)
        {
            if (c is 'カ' or 'サ' or 'タ' or 'ナ' or 'ハ' or 'マ' or 'ャ' or 'ラ' or 'ワ' or 'ヵ')
                katakanaChars.Append('\n');
            katakanaChars.Append('\'');
            if (c is '\\' or '\'')
                katakanaChars.Append('\\');
            katakanaChars.Append(c);
            katakanaChars.Append('\'');
            katakanaChars.Append(',');
            katakanaChars.Append(' ');

            markdownPair.Add((span[..length].ToString(), c.ToString()));
        }
    }

    Console.WriteLine("ひらがな");
    Console.WriteLine(hiraganaChars.ToString());
    Console.WriteLine("カタカナ");
    Console.WriteLine(katakanaChars.ToString());

    var builder = new StringBuilder(1000);
    var count = 0;
    builder.AppendLine("|H|K||H|K||H|K||H|K||H|K|");
    builder.AppendLine("|-|-|-| -|-|-| -|-|-| -|-|-| -|-|");
    foreach (var pair in markdownPair.OrderBy(p => p.Item1[0]))
    {
        if (count >= 5)
        {
            builder.Append('\n');
            count = 0;
        }
        builder.Append('|');
        builder.Append(pair.Item1);
        builder.Append('|');
        builder.Append(pair.Item2);
        builder.Append('|');
        count++;
    }

    while (count <= 5)
    {
        builder.Append('|');
        builder.Append('|');
        builder.Append('|');
        count++;
    }
    Console.WriteLine(builder.ToString());
}
