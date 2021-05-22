﻿// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit.Blueprints.Lyrics;
using osu.Game.Rulesets.Karaoke.Edit.Blueprints.Notes;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Screens.Edit.Compose.Components;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeBlueprintContainer : ComposeBlueprintContainer
    {
        public KaraokeBlueprintContainer(HitObjectComposer composer)
            : base(composer)
        {
        }

        public override OverlaySelectionBlueprint CreateBlueprintFor(DrawableHitObject hitObject)
        {
            switch (hitObject)
            {
                case DrawableNote note:
                    return new NoteSelectionBlueprint(note);

                case DrawableLyric lyric:
                    return new LyricSelectionBlueprint(lyric);

                default:
                    throw new IndexOutOfRangeException(nameof(hitObject));
            }
        }

        protected override SelectionHandler<HitObject> CreateSelectionHandler() => new KaraokeSelectionHandler();
    }
}
