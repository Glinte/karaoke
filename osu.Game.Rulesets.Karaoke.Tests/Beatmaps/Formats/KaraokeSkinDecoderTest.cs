﻿// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Game.IO;
using osu.Game.Rulesets.Karaoke.Beatmaps.Formats;
using osu.Game.Rulesets.Karaoke.Tests.Resources;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Tests.Beatmaps.Formats
{
    [TestFixture]
    public class KaraokeSkinDecoderTest
    {
        [Test]
        public void TestDecodeKaraokeSkin()
        {
            using (var resStream = TestResources.OpenSkinResource("default"))
            using (var stream = new LineBufferedReader(resStream))
            {
                var decoder = new KaraokeSkinDecoder();
                var skin = decoder.Decode(stream);

                // Checking font decode result
                var firstDecodedFont = skin.Styles.FirstOrDefault();
                Assert.IsNotNull(firstDecodedFont);
                Assert.AreEqual(firstDecodedFont.Name, "標準配色");

                // TestShader
                var shaders = firstDecodedFont.LeftLyricTextShaders;
                Assert.NotNull(shaders);

                // Test step shader.
                var stepShader = shaders.FirstOrDefault() as StepShader;
                Assert.NotNull(stepShader);
                Assert.AreEqual(stepShader.Name, "HelloShader");
                Assert.AreEqual(stepShader.StepShaders.Count, 2);

                // Test outline shader.
                var outlineShader = stepShader.StepShaders.FirstOrDefault() as OutlineShader;
                Assert.NotNull(outlineShader);
                Assert.AreEqual(outlineShader.OutlineColour, new Color4(255, 255, 255, 255));
                Assert.AreEqual(outlineShader.Radius, 10);

                // Test shader convert result.
                var shadowShader = stepShader.StepShaders.LastOrDefault() as ShadowShader;
                Assert.NotNull(shadowShader);
                Assert.AreEqual(shadowShader.ShadowOffset, new Vector2(3));

                // Checking layout decode result
                var firstDecodedLayout = skin.Layouts.FirstOrDefault();
                Assert.NotNull(firstDecodedLayout);
                Assert.AreEqual(firstDecodedLayout.Name, "下-1");
                Assert.AreEqual(firstDecodedLayout.Alignment, Anchor.BottomRight);
                Assert.AreEqual(firstDecodedLayout.HorizontalMargin, 30);
                Assert.AreEqual(firstDecodedLayout.VerticalMargin, 45);
                Assert.AreEqual(firstDecodedLayout.Continuous, false);
                Assert.AreEqual(firstDecodedLayout.SmartHorizon, KaraokeTextSmartHorizon.Multi);
                Assert.AreEqual(firstDecodedLayout.LyricsInterval, 4);
                Assert.AreEqual(firstDecodedLayout.RubyInterval, 2);
                Assert.AreEqual(firstDecodedLayout.RubyAlignment, LyricTextAlignment.Auto);
                Assert.AreEqual(firstDecodedLayout.RomajiAlignment, LyricTextAlignment.Auto);
                Assert.AreEqual(firstDecodedLayout.RubyMargin, 4);
                Assert.AreEqual(firstDecodedLayout.RomajiMargin, 0);

                // Checking note decode result
                var firstDecodedNoteSkin = skin.NoteSkins.FirstOrDefault();
                Assert.NotNull(firstDecodedNoteSkin);
                Assert.AreEqual(firstDecodedNoteSkin.Name, "Note-1");
                Assert.AreEqual(firstDecodedNoteSkin.NoteColor, new Color4(68, 170, 221, 255));
                Assert.AreEqual(firstDecodedNoteSkin.BlinkColor, new Color4(255, 102, 170, 255));
                Assert.AreEqual(firstDecodedNoteSkin.TextColor, new Color4(255, 255, 255, 255));
                Assert.AreEqual(firstDecodedNoteSkin.BoldText, true);
            }
        }
    }
}
