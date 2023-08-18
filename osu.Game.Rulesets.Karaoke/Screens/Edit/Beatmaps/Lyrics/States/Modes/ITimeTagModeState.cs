// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Screens.Edit.Beatmaps.Lyrics.States.Modes;

public interface ITimeTagModeState : IHasEditStep<TimeTagEditStep>, IHasBlueprintSelection<TimeTag>
{
    BindableFloat BindableRecordZoom { get; }

    BindableFloat BindableAdjustZoom { get; }
}
