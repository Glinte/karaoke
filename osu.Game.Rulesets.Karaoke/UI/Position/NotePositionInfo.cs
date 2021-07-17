﻿// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Skinning;

namespace osu.Game.Rulesets.Karaoke.UI.Position
{
    public class NotePositionInfo : Component, INotePositionInfo
    {
        private readonly Bindable<IPositionCalculator> position = new Bindable<IPositionCalculator>();
        public IBindable<IPositionCalculator> Position => position;

        [BackgroundDependencyLoader]
        private void load(IBeatmap beatmap, ISkinSource skin)
        {
            // todo : apply the algorithm.
            Position.Value = new PositionCalculator(9);
        }
    }
}
