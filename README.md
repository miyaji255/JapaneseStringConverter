# JapaneseStringConverter

[![CI](https://github.com/miyaji255/JapaneseStringConverter/actions/workflows/action.yml/badge.svg?branch=main)](https://github.com/miyaji255/JapaneseStringConverter/actions/workflows/action.yml)

JapaneseStringConverter は高速で低アロケーションな日本語変換ライブラリです。

|                          Method |         Mean |      Error |    StdDev |   Gen0 | Allocated |
|-------------------------------- |-------------:|-----------:|----------:|-------:|----------:|
|   JapaneseStringConverterNarrow |    339.00 ns |   5.582 ns |  4.662 ns | 0.0348 |     456 B |
|                   StrConvNarrow |  7,396.62 ns |  93.722 ns | 87.668 ns | 0.0916 |    1288 B |
|                    KanaxsNarrow |    253.73 ns |   4.408 ns |  6.731 ns | 0.0620 |     816 B |
|                KanaxsNarrowKana |    489.60 ns |   2.886 ns |  2.410 ns | 0.0954 |    1248 B |
|                KanaxsNarrowBoth |    829.58 ns |  12.618 ns | 11.185 ns | 0.1650 |    2168 B |
|     JapaneseStringConverterWide |    356.50 ns |   1.330 ns |  1.244 ns | 0.0310 |     408 B |
|                     StrConvWide | 37,923.21 ns | 101.514 ns | 84.769 ns | 0.0610 |    1240 B |
|                      KanaxsWide |    251.36 ns |   0.475 ns |  0.421 ns | 0.0701 |     920 B |
|                  KanaxsWideKana |    460.14 ns |   1.699 ns |  1.419 ns | 0.0663 |     872 B |
|                  KanaxsWideBoth |    686.99 ns |   2.169 ns |  1.812 ns | 0.1287 |    1688 B |
| JapaneseStringConverterHiragana |    121.97 ns |   0.348 ns |  0.291 ns | 0.0184 |     240 B |
|                 StrConvHiragana |  1,812.72 ns |   3.799 ns |  3.553 ns | 0.0782 |    1032 B |
|                  KanaxsHiragana |    125.87 ns |   0.983 ns |  0.919 ns | 0.0513 |     672 B |
| JapaneseStringConverterKatakana |    111.89 ns |   1.474 ns |  1.306 ns | 0.0176 |     232 B |
|                 StrConvKatakana |  1,863.01 ns |   2.991 ns |  2.651 ns | 0.0801 |    1056 B |
|                  KanaxsKatakana |     85.51 ns |   0.428 ns |  0.380 ns | 0.0367 |     480 B |
