// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Beatmaps.Stages;

namespace osu.Game.Rulesets.Karaoke.Objects.Stages;

public abstract class StageEffectApplier
{
    private readonly IEnumerable<StageElement> elements;
    private readonly StageDefinition definition;

    protected StageEffectApplier(IEnumerable<StageElement> elements, StageDefinition definition)
    {
        this.elements = elements;
        this.definition = definition;
    }
}
