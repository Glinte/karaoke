﻿// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Overlays;
using osu.Game.Rulesets.Karaoke.Edit.ChangeHandlers.Languages;
using osu.Game.Rulesets.Karaoke.Edit.ChangeHandlers.Lyrics;
using osu.Game.Rulesets.Karaoke.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Screens.Edit;

namespace osu.Game.Rulesets.Karaoke.Edit.Translate
{
    [Cached(typeof(ITranslateInfoProvider))]
    public class TranslateScreen : KaraokeEditorScreen, ITranslateInfoProvider
    {
        [Resolved]
        private EditorBeatmap beatmap { get; set; }

        [Cached]
        protected readonly OverlayColourProvider ColourProvider;

        [Cached(typeof(ILanguagesChangeHandler))]
        private readonly LanguagesChangeHandler languagesChangeHandler;

        [Cached(typeof(ILyricTranslateChangeHandler))]
        private readonly LyricTranslateChangeHandler lyricTranslateChangeHandler;

        public TranslateScreen()
            : base(KaraokeEditorScreenMode.Translate)
        {
            ColourProvider = new OverlayColourProvider(OverlayColourScheme.Green);
            AddInternal(languagesChangeHandler = new LanguagesChangeHandler());
            AddInternal(lyricTranslateChangeHandler = new LyricTranslateChangeHandler());
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours, DialogOverlay dialogOverlay, LanguageSelectionDialog languageSelectionDialog)
        {
            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(50),
                Child = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    CornerRadius = 10,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = colours.GreySeafoamDark,
                            RelativeSizeAxes = Axes.Both,
                        },
                        new SectionsContainer<Container>
                        {
                            FixedHeader = new TranslateScreenHeader(),
                            RelativeSizeAxes = Axes.Both,
                            Children = new Container[]
                            {
                                new TranslateEditSection
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                },
                            }
                        },
                    }
                }
            });

            // ask only once if contains no language after switch to translate editor.
            bool alreadyAsked;
            languagesChangeHandler.Languages.BindCollectionChanged((_, _) =>
            {
                alreadyAsked = true;

                if (languagesChangeHandler.Languages.Count == 0 && !alreadyAsked)
                {
                    dialogOverlay.Push(new CreateNewLanguagePopupDialog(isOk =>
                    {
                        if (isOk)
                        {
                            languageSelectionDialog.Show();
                        }
                    }));
                }
            }, true);
        }

        public string GetLyricTranslate(Lyric lyric, CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
                throw new ArgumentNullException(nameof(cultureInfo));

            return lyric.Translates.TryGetValue(cultureInfo, out string translate) ? translate : null;
        }

        public IEnumerable<Lyric> TranslatableLyrics => beatmap.HitObjects.OfType<Lyric>();

        internal class TranslateScreenHeader : OverlayHeader
        {
            protected override OverlayTitle CreateTitle() => new TranslateScreenTitle();

            private class TranslateScreenTitle : OverlayTitle
            {
                public TranslateScreenTitle()
                {
                    Title = "translate";
                    Description = "create translation of your beatmap";
                    IconTexture = "Icons/Hexacons/social";
                }
            }
        }
    }
}
