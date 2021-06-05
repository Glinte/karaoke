﻿// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Overlays;
using osu.Game.Rulesets.Karaoke.Online.API.Requests.Responses;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Overlays.Changelog.Sidebar
{
    public class YearsPanel : CompositeDrawable
    {
        private readonly Bindable<APIChangelogSidebar> metadata = new Bindable<APIChangelogSidebar>();

        private FillFlowContainer yearsFlow;

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider overlayColours, Bindable<APIChangelogSidebar> metadata)
        {
            this.metadata.BindTo(metadata);

            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
            Masking = true;
            CornerRadius = 6;
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = overlayColours.Background3
                },
                new Container
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding(5),
                    Child = yearsFlow = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Spacing = new Vector2(0, 5)
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            metadata.BindValueChanged(_ => recreateDrawables(), true);
        }

        private void recreateDrawables()
        {
            yearsFlow.Clear();

            if (metadata.Value == null)
            {
                Hide();
                return;
            }

            var currentYear = metadata.Value.CurrentYear;

            foreach (var y in metadata.Value.Years)
                yearsFlow.Add(new YearButton(y, y == currentYear));

            Show();
        }

        public class YearButton : OsuHoverContainer
        {
            public int Year { get; }

            [Resolved(canBeNull: true)]
            private NewsOverlay overlay { get; set; }

            private readonly bool isCurrent;

            public YearButton(int year, bool isCurrent)
            {
                Year = year;
                this.isCurrent = isCurrent;

                RelativeSizeAxes = Axes.X;
                Width = 0.25f;
                Height = 15;

                Child = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = OsuFont.GetFont(size: 12, weight: isCurrent ? FontWeight.SemiBold : FontWeight.Medium),
                    Text = year.ToString()
                };
            }

            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            {
                IdleColour = isCurrent ? Color4.White : colourProvider.Light2;
                HoverColour = isCurrent ? Color4.White : colourProvider.Light1;
                Action = () =>
                {
                    if (!isCurrent)
                        overlay?.ShowYear(Year);
                };
            }
        }
    }
}
