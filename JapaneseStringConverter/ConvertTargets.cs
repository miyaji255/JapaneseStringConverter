using System;

namespace JapaneseStringConverter
{
    /// <summary>
    /// StringUtitlity.Convert 関数を呼び出す際に、どのタイプの変換を行うかを示す。
    /// </summary>
    [Flags]
    public enum ConvertTargets
    {
        /// <summary>
        /// 変換しません
        /// </summary>
        None = 0,

        /// <summary>
        /// 大文字に変換します
        /// </summary>
        Uppercase = 1,

        /// <summary>
        /// 小文字に変換します
        /// </summary>
        Lowercase = 2,

        /// <summary>
        /// 全角文字に変換します
        /// </summary>
        Wide = 4,

        /// <summary>
        /// 半角文字に変換します
        /// </summary>
        Narrow = 8,

        /// <summary>
        /// カタカナに変換します
        /// </summary>
        Katakana = 16,

        /// <summary>
        /// ひらがなに変換します
        /// </summary>
        Hiragana = 32,
    }
}
