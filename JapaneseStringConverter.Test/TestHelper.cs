using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapaneseStringConverter.Test
{
    internal static class TestHelper
    {
        internal static string[] ToHexString(this string source)
        {
            return source.Select(c => $"{c}:{(int)c:X}").ToArray();
        }
    }
}
