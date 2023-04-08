using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace JapaneseStringConverter.Internal
{
    internal static class UnicodeStringConverter
    {
        private const char CombiningDakuten = '\u3099';
        private const char CombiningHandakuten = '\u309A';


        internal static int ToNarrow(ReadOnlySpan<char> source, Span<char> destination)
        {
            if (source.Length > destination.Length)
                return -1;

            var length = 0;
            try
            {
                foreach (var target in source)
                {
                    switch (target)
                    {
                        case < '←' or > '￦':
                            destination[length++] = target;
                            continue;
                        case <= '↓':
                            destination[length++] = (char)(target - '←' + '￩');
                            continue;
                        case < '■': // U+25A0
                            destination[length++] = target;
                            continue;
                        case '■': // U+25A0
                            destination[length++] = '￭';
                            continue;
                        case < '○': // U+25CB
                            destination[length++] = target;
                            continue;
                        case '○': // U+25CB
                            destination[length++] = '￮';
                            continue;
                        case < '\u3000':
                            destination[length++] = target;
                            continue;
                        case '　': // U+3000 space
                            destination[length++] = ' ';
                            continue;
                        case '、': // U+3001
                            destination[length++] = '､';
                            continue;
                        case '。': // U+3002
                            destination[length++] = '｡';
                            continue;
                        case < '「': // U+3003 ~ U+300B
                            destination[length++] = target;
                            continue;
                        case '「': // U+300C
                            destination[length++] = '｢';
                            continue;
                        case '」': // U+300D
                            destination[length++] = '｣';
                            continue;
                        case < CombiningDakuten: // U+300E ~ U+3098
                            destination[length++] = target;
                            continue;
                        case CombiningDakuten: // U+3099 合字用の濁点
                            if (length == 0)
                            {
                                destination[length++] = target;
                                continue;
                            }
                            else
                            {
                                switch (destination[length - 1])
                                {
                                    case < 'ｦ':
                                        destination[length++] = target;
                                        continue;
                                    case < 'ﾝ':
                                        destination[length++] = 'ﾞ';
                                        continue;
                                    default:
                                        destination[length++] = target;
                                        continue;
                                }
                            }
                        case CombiningHandakuten: // U+309A 合字用の半濁点
                            if (length == 0)
                            {
                                destination[length++] = target;
                                continue;
                            }
                            else
                            {
                                switch (destination[length - 1])
                                {
                                    case < 'ｦ':
                                        destination[length++] = target;
                                        continue;
                                    case < 'ﾝ':
                                        destination[length++] = 'ﾟ';
                                        continue;
                                    case >= 'ﾝ':
                                        destination[length++] = target;
                                        continue;
                                }
                            }
                        case < '゠': // U+300E ~ U+309F
                            destination[length++] = target;
                            continue;
                        case '゠': // U+30A0
                            destination[length++] = '=';
                            continue;
                        case <= 'オ': // U+30A1 ~ U+30AA
                            destination[length++] = (target & 1) == 0
                                ? (char)((target - 'ア' >> 1) + 'ｱ')
                                : (char)((target - 'ァ' >> 1) + 'ｧ');
                            continue;
                        case <= 'ヂ': // U+30AB ~ U+30C2
                            if ((target & 1) == 0)
                            {
                                destination[length++] = (char)(((target - 'ガ') >> 1) + 'ｶ');
                                destination[length++] = 'ﾞ';
                                continue;
                            }
                            else
                            {
                                destination[length++] = (char)(((target - 'カ') >> 1) + 'ｶ');
                                continue;
                            }
                        case 'ッ': // U+30C3
                            destination[length++] = 'ｯ';
                            continue;
                        case <= 'ド': // U+30C4 ~ U+30C9
                            if ((target & 1) == 0)
                            {
                                destination[length++] = (char)(((target - 'ツ') >> 1) + 'ﾂ');
                                continue;
                            }
                            else
                            {
                                destination[length++] = (char)((target - 'ヅ' >> 1) + 'ﾂ');
                                destination[length++] = 'ﾞ';
                                continue;
                            }
                        case <= 'ノ': // U+30CA ~ U+30CE
                            destination[length++] = (char)(target - 'ナ' + 'ﾅ');
                            continue;
                        case <= 'ポ': // U+30CF ~ U+30DD
                            var temp = target % 3;
                            if (temp == 0)
                            {
                                destination[length++] = (char)((target - 'ハ') / 3 + 'ﾊ');
                                continue;
                            }
                            else if (temp == 1)
                            {
                                destination[length++] = (char)((target - 'バ') / 3 + 'ﾊ');
                                destination[length++] = 'ﾞ';
                                continue;
                            }
                            else
                            {
                                destination[length++] = (char)((target - 'パ') / 3 + 'ﾊ');
                                destination[length++] = 'ﾟ';
                                continue;
                            }
                        case <= 'モ': // U+30DE ~ U+30E2
                            destination[length++] = (char)(target - 'マ' + 'ﾏ');
                            continue;
                        case <= 'ヨ': // U+30E3 ~ U+30E8
                            destination[length++] = (target & 1) == 0
                                ? (char)((target - 'ヤ' >> 1) + 'ﾔ')
                                : (char)((target - 'ャ' >> 1) + 'ｬ');
                            continue;
                        case <= 'ロ': // U+30E9 ~ U+30ED
                            destination[length++] = (char)(target - 'ラ' + 'ﾗ');
                            continue;
                        case 'ヮ': // U+30EE
                            destination[length++] = 'ヮ';
                            continue;
                        case 'ワ': // U+30EF
                            destination[length++] = 'ﾜ';
                            continue;
                        case 'ヰ': // U+30F0
                        case 'ヱ': // U+30F1
                            destination[length++] = target;
                            continue;
                        case 'ヲ': // U+30F2
                            destination[length++] = 'ｦ';
                            continue;
                        case 'ン': // U+30F3
                            destination[length++] = 'ﾝ';
                            continue;
                        case 'ヴ': // U+30F4
                            destination[length++] = 'ｳ';
                            destination[length++] = 'ﾞ';
                            continue;
                        case 'ヵ': // U+30F5 
                        case 'ヶ': // U+30F6
                            destination[length++] = target;
                            continue;
                        case 'ヷ': // U+30F7
                            destination[length++] = 'ﾜ';
                            destination[length++] = 'ﾞ';
                            continue;
                        case 'ヸ': // U+30F8
                        case 'ヹ': // U+30F9
                            destination[length++] = target;
                            continue;
                        case 'ヺ': // U+30FA
                            destination[length++] = 'ｦ';
                            destination[length++] = 'ﾞ';
                            continue;
                        case '・': // U+30FB
                            destination[length++] = '･';
                            continue;
                        case 'ー': // U+30FC
                            destination[length++] = 'ｰ';
                            continue;
                        case < '！':
                            destination[length++] = target;
                            continue;
                        case <= '～': // U+FF01 ~ // U+FF5E
                            destination[length++] = (char)(target - '！' + '!');
                            continue;
                        case < '￠':
                            destination[length++] = target;
                            continue;
                        case '￠': // U+FFE0
                        case '￡': // U+FFE1
                            destination[length++] = (char)(target - '￠' + '¢');
                            continue;
                        case '￢': // U+FFE2
                            destination[length++] = '¬';
                            continue;
                        case '￣': // U+FFE3
                            destination[length++] = '¯';
                            continue;
                        case '￤': // U+FFE4
                            destination[length++] = '¦';
                            continue;
                        case '￥': // U+FFE5
                            destination[length++] = '\\';
                            continue;
                        case '￦': // U+FFE6
                            destination[length++] = '₩';
                            continue;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
            return length;
        }

        internal static int ToWide(ReadOnlySpan<char> source, Span<char> destination)
        {
            var length = 0;
            try
            {
                foreach (var target in source)
                {
                    switch (target)
                    {
                        case > '￮':
                            destination[length++] = target;
                            continue;
                        case < ' ': // U+0020
                            destination[length++] = target;
                            continue;
                        case ' ': // U+0020 space
                            destination[length++] = '\u3000';
                            continue;
                        case <= '~': // U+0021 ~ U+007E
                            // U+201D足す
                            destination[length++] = target == '\\' ? '￥' : (char)(target - '!' + '！');
                            continue;
                        case < '\u00A2':
                            destination[length++] = target;
                            continue;
                        case '¢': // U+00A2
                            destination[length++] = '￠';
                            continue;
                        case '£': // U+00A3
                            destination[length++] = '￡';
                            continue;
                        case '¤': // U+00A4
                            destination[length++] = '¤';
                            continue;
                        case '¥': // U+00A5
                            destination[length++] = '￥';
                            continue;
                        case '¦': // U+00A6
                            destination[length++] = '￤';
                            continue;
                        case < '\u00AC':
                            destination[length++] = target;
                            continue;
                        case '¬': // U+00AC
                            destination[length++] = '￢';
                            continue;
                        case '¯': // U+00AF
                            destination[length++] = '￣';
                            continue;
                        case '₩': // 20A9
                            destination[length++] = '￦';
                            continue;
                        case < '｡': // U+FF61
                            destination[length++] = target;
                            continue;
                        case '｡': // U+FF61
                            destination[length++] = '。';
                            continue;
                        case '｢': // U+FF62
                            destination[length++] = '「';
                            continue;
                        case '｣': // U+FF63
                            destination[length++] = '」';
                            continue;
                        case '､': // U+FF64
                            destination[length++] = '、';
                            continue;
                        case '･': // U+FF65
                            destination[length++] = '・';
                            continue;
                        case 'ｦ': // U+FF66
                            destination[length++] = 'ヲ';
                            continue;
                        case <= 'ｫ': // U+FF67 ~ U+FF6B
                            destination[length++] = (char)((target - 'ｧ' << 1) + 'ァ');
                            continue;
                        case <= 'ｮ': // U+FF6C ~ U+FF6E
                            destination[length++] = (char)((target - 'ｬ' << 1) + 'ャ');
                            continue;
                        case 'ｯ': // U+FF6F
                            destination[length++] = 'ッ';
                            continue;
                        case 'ｰ': // U+FF70
                            destination[length++] = 'ー';
                            continue;
                        case <= 'ｵ': // U+FF71 ~ U+FF75
                            destination[length++] = (char)((target - 'ｱ' << 1) + 'ア');
                            continue;
                        case <= 'ﾁ': // U+FF81
                            destination[length++] = (char)((target - 'ｶ' << 1) + 'カ');
                            continue;
                        case <= 'ﾄ': // U+FF82 ~ U+FF84
                            destination[length++] = (char)((target - 'ﾂ' << 1) + 'ツ');
                            continue;
                        case <= 'ﾊ': // U+FF85 ~ U+FF8A
                            destination[length++] = (char)(target - 'ﾅ' + 'ナ');
                            continue;
                        case <= 'ﾎ': // U+FF8B ~ U+FF8E
                            destination[length++] = (char)((target - 'ﾋ') * 3 + 'ヒ');
                            continue;
                        case <= 'ﾓ': // U+FF8F ~ U+FF93
                            destination[length++] = (char)(target - 'ﾏ' + 'マ');
                            continue;
                        case <= 'ﾖ': // U+FF94 ~ U+FF96
                            destination[length++] = (char)((target - 'ﾔ' << 1) + 'ヤ');
                            continue;
                        case <= 'ﾛ': // U+FF97 ~ U+FF9B
                            destination[length++] = (char)(target - 'ﾗ' + 'ラ');
                            continue;
                        case 'ﾜ': // U+FF9C
                            destination[length++] = 'ワ';
                            continue;
                        case 'ﾝ': // U+FF9D
                            destination[length++] = 'ン';
                            continue;
                        case 'ﾞ': // U+FF9E
                            if (length == 0)
                            {
                                destination[length++] = 'ﾞ';
                                continue;
                            }
                            else
                            {
                                var last = destination[length - 1];
                                switch (last)
                                {
                                    case < 'ァ': // 半角カタカナじゃない
                                        destination[length++] = 'ﾞ';
                                        continue;
                                    case < 'ウ':
                                        destination[length++] = CombiningDakuten;
                                        continue;
                                    case 'ウ':
                                        destination[length - 1] = 'ヴ';
                                        continue;
                                    case <= 'オ':
                                        destination[length++] = CombiningDakuten;
                                        continue;
                                    case < 'ッ':
                                        if ((last & 1) == 0)
                                        {
                                            destination[length++] = CombiningDakuten;
                                            continue;
                                        }
                                        else
                                        {
                                            destination[length - 1] += (char)1;
                                            continue;
                                        }
                                    case 'ッ':
                                        destination[length++] = CombiningDakuten;
                                        continue;
                                    case <= 'ト':
                                        if ((last & 1) == 0)
                                        {
                                            destination[length - 1] += (char)1;
                                            continue;
                                        }
                                        else
                                        {
                                            destination[length++] = CombiningDakuten;
                                            continue;
                                        }
                                    case <= 'ノ':
                                        destination[length++] = CombiningDakuten;
                                        continue;
                                    case <= 'ホ':
                                        if ((last % 3) == 0)
                                        {
                                            destination[length - 1] += (char)1;
                                            continue;
                                        }
                                        else
                                        {
                                            destination[length++] = CombiningDakuten;
                                            continue;
                                        }
                                    case <= 'ヮ':
                                        destination[length++] = CombiningDakuten;
                                        continue;
                                    case <= 'ヲ':
                                        destination[length - 1] += (char)8;
                                        continue;
                                    case <= 'ヺ':
                                        destination[length++] = CombiningDakuten;
                                        continue;
                                    case < 'ヽ':
                                        destination[length++] = 'ﾞ';
                                        continue;
                                    case 'ヽ':
                                        destination[length++] = CombiningDakuten;
                                        continue;
                                    case > 'ヽ':
                                        destination[length++] = 'ﾞ';
                                        continue;
                                }
                            }
                        case 'ﾟ': // U+FF9F
                            if (length == 0)
                            {
                                destination[length++] = 'ﾟ';
                                continue;
                            }
                            else
                            {
                                var last = destination[length - 1];
                                switch (last)
                                {
                                    case < 'ァ':
                                        destination[length++] = 'ﾟ';
                                        continue;
                                    case < 'ハ':
                                        destination[length++] = CombiningHandakuten;
                                        continue;
                                    case <= 'ホ':
                                        if (last % 3 == 0)
                                        {
                                            destination[length - 1] += (char)2;
                                            continue;
                                        }
                                        else
                                        {
                                            destination[length++] = CombiningHandakuten;
                                            continue;
                                        }
                                    case > 'ホ':
                                        destination[length++] = CombiningHandakuten;
                                        continue;
                                }
                            }
                        case < '￩': // U+FFE9
                            destination[length++] = target;
                            continue;
                        case <= '￬': // U+FFE9 ~ U+FFEC
                            destination[length++] = (char)(target - '￩' + '←');
                            continue;
                        case '￭': // U+FFED
                            destination[length++] = '■';
                            continue;
                        case '￮': // U+FFEE
                            destination[length++] = '○';
                            continue;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
            return length;
        }

        internal static int ToHiragana(ReadOnlySpan<char> source, ref Span<char> destination, bool ensured)
        {
            var length = 0;
            try
            {
                foreach (var target in source)
                {
                    switch (target)
                    {
                        case > 'ヾ': // > U+30FE orでつなげるとswitchが最適化されない
                            destination[length++] = target;
                            continue;
                        case < 'ァ': // < U+30A1
                            destination[length++] = target;
                            continue;
                        case <= 'ヶ': // U+30A1 ~ U+30F6
                            destination[length++] = (char)('ぁ' - 'ァ' + target);
                            continue;
                        case <= 'ヺ': // U+30F7 ~ U+30FA
                            destination[length++] = (char)('わ' - 'ヷ' + target);
                            if (!ensured)
                            {
                                EnsureForHiragana(ref destination, ((source.Length - length) << 1) + source.Length);
                                ensured = true;
                            }
                            destination[length++] = CombiningDakuten;
                            continue;
                        case < 'ヽ': // U+30FB, U+30FC
                            destination[length++] = target;
                            continue;
                        case 'ヽ': // U+30FD
                            destination[length++] = 'ゝ';
                            continue;
                        case 'ヾ': // U+30FE
                            destination[length++] = 'ゞ';
                            continue;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
            return length;
        }

        internal static int ToKatakana(ReadOnlySpan<char> source, Span<char> destination)
        {
            var length = 0;
            try
            {
                foreach (var target in source)
                {
                    switch (target)
                    {
                        case < 'ぁ':
                            destination[length++] = target;
                            continue;
                        case <= 'ゖ':
                            destination[length++] = (char)('ァ' - 'ぁ' + target);
                            continue;
                        case < CombiningDakuten:
                            destination[length++] = target;
                            continue;
                        case CombiningDakuten:
                            if (length == 0)
                            {
                                destination[length++] = target;
                            }
                            else
                            {
                                switch (destination[length - 1])
                                {
                                    case < 'ワ':
                                        destination[length++] = target;
                                        continue;
                                    case <= 'ヲ':
                                        destination[length - 1] += (char)('ヷ' - 'ワ');
                                        continue;
                                    case > 'ヲ':
                                        destination[length++] = target;
                                        continue;
                                }
                            }
                            continue;
                        case < 'ゝ':
                            destination[length++] = target;
                            continue;
                        case 'ゝ':
                            destination[length++] = 'ヽ';
                            continue;
                        case 'ゞ':
                            destination[length++] = 'ヾ';
                            continue;
                        case > 'ゞ':
                            destination[length++] = target;
                            continue;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
            return length;
        }

        private static void EnsureForHiragana(ref Span<char> span, int length)
        {
            if (span.Length >= length)
                return;

            Span<char> temp = new char[length];
            span.CopyTo(temp);
            span = temp;
        }
    }
}
