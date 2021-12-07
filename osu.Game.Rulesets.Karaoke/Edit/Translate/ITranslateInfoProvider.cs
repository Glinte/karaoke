// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Globalization;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Edit.Translate
{
    public interface ITranslateInfoProvider
    {
        string GetLyricTranslate(Lyric lyric, CultureInfo cultureInfo);

        IEnumerable<Lyric> TranslatableLyrics { get; }
    }
}
