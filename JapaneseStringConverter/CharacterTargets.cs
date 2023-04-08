using System.Collections.ObjectModel;

#if !NETFRAMEWORK
using System;
using System.Collections.Immutable;
#endif

namespace JapaneseStringConverter
{
    /// <summary>
    /// 変換可能な文字を返す配列を提供します
    /// </summary>
    public static class CharacterTargets
    {
#if !NETFRAMEWORK
        /// <summary>
        /// 全角化できる半角文字を返します
        /// </summary>
        public static readonly ImmutableArray<char> Narrow = ImmutableArray.Create(_narrow);
#endif

        /// <summary>
        /// 全角化できる半角文字を返します
        /// </summary>
#if !NETFRAMEWORK
        [Obsolete($"{nameof(CharacterTargets)}.{nameof(Narrow)} を使用してください")]
#endif
        public static readonly ReadOnlyCollection<char> ReadOnlyNarrow = new(_narrow!);

        private static readonly char[] _narrow =
        {
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            ':', ';', '<', '=', '>', '?', '@',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '[', '\\', ']', '^', '_', '`',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '{', '|', '}', '~', '¢', '£', '¥', '¦', '¬', '¯', '₩', '｡', '｢', '｣', '､', '･',
            'ｦ',
            'ｧ', 'ｨ', 'ｩ', 'ｪ', 'ｫ', 'ｬ', 'ｭ', 'ｮ', 'ｯ', 'ｰ',
            'ｱ', 'ｲ', 'ｳ', 'ｴ', 'ｵ',
            'ｶ', 'ｷ', 'ｸ', 'ｹ', 'ｺ',
            'ｻ', 'ｼ', 'ｽ', 'ｾ', 'ｿ',
            'ﾀ', 'ﾁ', 'ﾂ', 'ﾃ', 'ﾄ',
            'ﾅ', 'ﾆ', 'ﾇ', 'ﾈ', 'ﾉ',
            'ﾊ', 'ﾋ', 'ﾌ', 'ﾍ', 'ﾎ',
            'ﾏ', 'ﾐ', 'ﾑ', 'ﾒ', 'ﾓ',
            'ﾔ', 'ﾕ', 'ﾖ',
            'ﾗ', 'ﾘ', 'ﾙ', 'ﾚ', 'ﾛ',
            'ﾜ', 'ﾝ',
            '￩', '￪', '￫', '￬', '￭', '￮',
        };

#if !NETFRAMEWORK
        /// <summary>
        /// 全角化できる半角文字を返します
        /// </summary>
        public static readonly ImmutableArray<char> Wide = ImmutableArray.Create(_wide);
#endif

        /// <summary>
        /// 半角化できる全角文字を返します
        /// </summary>
#if !NETFRAMEWORK
        [Obsolete($"{nameof(CharacterTargets)}.{nameof(Wide)} を使用してください")]
#endif
        public static readonly ReadOnlyCollection<char> ReadOnlyWide = new(_wide!);

        private static readonly char[] _wide =
        {
            '←', '↑', '→', '↓', '■', '○',
            '　', '、', '。', '「', '」', '゠',
            'ァ', 'ア', 'ィ', 'イ', 'ゥ', 'ウ', 'ェ', 'エ', 'ォ', 'オ',
            'カ', 'ガ', 'キ', 'ギ', 'ク', 'グ', 'ケ', 'ゲ', 'コ', 'ゴ',
            'サ', 'ザ', 'シ', 'ジ', 'ス', 'ズ', 'セ', 'ゼ', 'ソ', 'ゾ',
            'タ', 'ダ', 'チ', 'ヂ', 'ッ', 'ツ', 'ヅ', 'テ', 'デ', 'ト', 'ド',
            'ナ', 'ニ', 'ヌ', 'ネ', 'ノ',
            'ハ', 'バ', 'パ', 'ヒ', 'ビ', 'ピ', 'フ', 'ブ', 'プ', 'ヘ', 'ベ', 'ペ', 'ホ', 'ボ', 'ポ',
            'マ', 'ミ', 'ム', 'メ', 'モ',
            'ャ', 'ヤ', 'ュ', 'ユ', 'ョ', 'ヨ',
            'ラ', 'リ', 'ル', 'レ', 'ロ',
            'ワ', 'ヲ', 'ン', 'ヴ', 'ヷ', 'ヺ',
            '・', 'ー',
            '！', '＂', '＃', '＄', '％', '＆', '＇', '（', '）', '＊', '＋', '，', '－', '．', '／',
            '０', '１', '２', '３', '４', '５', '６', '７', '８', '９',
            '：', '；', '＜', '＝', '＞', '？', '＠',
            'Ａ', 'Ｂ', 'Ｃ', 'Ｄ', 'Ｅ', 'Ｆ', 'Ｇ', 'Ｈ', 'Ｉ', 'Ｊ', 'Ｋ', 'Ｌ', 'Ｍ', 'Ｎ', 'Ｏ', 'Ｐ', 'Ｑ', 'Ｒ', 'Ｓ', 'Ｔ', 'Ｕ', 'Ｖ', 'Ｗ', 'Ｘ', 'Ｙ', 'Ｚ',
            '［', '＼', '］', '＾', '＿', '｀',
            'ａ', 'ｂ', 'ｃ', 'ｄ', 'ｅ', 'ｆ', 'ｇ', 'ｈ', 'ｉ', 'ｊ', 'ｋ', 'ｌ', 'ｍ', 'ｎ', 'ｏ', 'ｐ', 'ｑ', 'ｒ', 'ｓ', 'ｔ', 'ｕ', 'ｖ', 'ｗ', 'ｘ', 'ｙ', 'ｚ',
            '｛', '｜', '｝', '～', '￠', '￡', '￢', '￣', '￤', '￥', '￦',
        };

#if !NETFRAMEWORK
        /// <summary>
        /// カタカナ化できるひらがなを返します
        /// </summary>
        public static readonly ImmutableArray<char> Hiragana = ImmutableArray.Create(_hiragana);
#endif

        /// <summary>
        /// カタカナ化できるひらがなを返します
        /// </summary>
#if !NETFRAMEWORK
        [Obsolete($"{nameof(CharacterTargets)}.{nameof(Hiragana)} を使用してください")]
#endif
        public static readonly ReadOnlyCollection<char> ReadOnlyHiragana = new(_hiragana!);

        private static readonly char[] _hiragana =
        {
            'ぁ', 'あ', 'ぃ', 'い', 'ぅ', 'う', 'ぇ', 'え', 'ぉ', 'お',
            'か', 'が', 'き', 'ぎ', 'く', 'ぐ', 'け', 'げ', 'こ', 'ご',
            'さ', 'ざ', 'し', 'じ', 'す', 'ず', 'せ', 'ぜ', 'そ', 'ぞ',
            'た', 'だ', 'ち', 'ぢ', 'っ', 'つ', 'づ', 'て', 'で', 'と', 'ど',
            'な', 'に', 'ぬ', 'ね', 'の',
            'は', 'ば', 'ぱ', 'ひ', 'び', 'ぴ', 'ふ', 'ぶ', 'ぷ', 'へ', 'べ', 'ぺ', 'ほ', 'ぼ', 'ぽ',
            'ま', 'み', 'む', 'め', 'も',
            'ゃ', 'や', 'ゅ', 'ゆ', 'ょ', 'よ',
            'ら', 'り', 'る', 'れ', 'ろ',
            'ゎ', 'わ', 'ゐ', 'ゑ', 'を', 'ん',
            'ゔ', 'ゕ', 'ゖ', 'ゝ', 'ゞ',
        };

#if !NETFRAMEWORK
        /// <summary>
        /// カタカナ化できるひらがなを返します
        /// </summary>
        public static readonly ImmutableArray<char> Katakana = ImmutableArray.Create(_katakana);
#endif

        /// <summary>
        /// ひらがな化できるカタカナを返します
        /// </summary>
#if !NETFRAMEWORK
        [Obsolete($"{nameof(CharacterTargets)}.{nameof(Katakana)} を使用してください")]
#endif
        public static readonly ReadOnlyCollection<char> ReadOnlyKatakana = new(_katakana!);

        private static readonly char[] _katakana =
        {
            'ァ', 'ア', 'ィ', 'イ', 'ゥ', 'ウ', 'ェ', 'エ', 'ォ', 'オ',
            'カ', 'ガ', 'キ', 'ギ', 'ク', 'グ', 'ケ', 'ゲ', 'コ', 'ゴ',
            'サ', 'ザ', 'シ', 'ジ', 'ス', 'ズ', 'セ', 'ゼ', 'ソ', 'ゾ',
            'タ', 'ダ', 'チ', 'ヂ', 'ッ', 'ツ', 'ヅ', 'テ', 'デ', 'ト', 'ド',
            'ナ', 'ニ', 'ヌ', 'ネ', 'ノ',
            'ハ', 'バ', 'パ', 'ヒ', 'ビ', 'ピ', 'フ', 'ブ', 'プ', 'ヘ', 'ベ', 'ペ', 'ホ', 'ボ', 'ポ',
            'マ', 'ミ', 'ム', 'メ', 'モ',
            'ャ', 'ヤ', 'ュ', 'ユ', 'ョ', 'ヨ',
            'ラ', 'リ', 'ル', 'レ', 'ロ', 'ヮ',
            'ワ', 'ヰ', 'ヱ', 'ヲ', 'ン', 'ヴ',
            'ヵ', 'ヶ', 'ヷ', 'ヸ', 'ヹ', 'ヺ', 'ヽ', 'ヾ',
        };
    }
}
