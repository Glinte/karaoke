﻿// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Screens.Edit.Beatmaps.Lyrics.CaretPosition;

public readonly struct CuttingCaretPosition : ICharGapCaretPosition, IComparable<CuttingCaretPosition>
{
    public CuttingCaretPosition(Lyric lyric, int index)
    {
        Lyric = lyric;
        CharGap = index;
    }

    public Lyric Lyric { get; }

    public int CharGap { get; }

    public int CompareTo(CuttingCaretPosition other)
    {
        if (Lyric != other.Lyric)
            throw new InvalidOperationException();

        return CharGap.CompareTo(other.CharGap);
    }

    public int CompareTo(IIndexCaretPosition? other)
    {
        if (other is not CuttingCaretPosition cuttingCaretPosition)
            throw new InvalidOperationException();

        return CompareTo(cuttingCaretPosition);
    }
}
