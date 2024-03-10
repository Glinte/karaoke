﻿// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Edit.Checks.Components;
using osu.Game.Rulesets.Karaoke.Edit.Checks.Issues;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Utils;

namespace osu.Game.Rulesets.Karaoke.Edit.Checks;

public class CheckLyricRubyTag : CheckHitObjectProperty<Lyric>
{
    protected override string Description => "Lyric with invalid ruby tag.";

    public override IEnumerable<IssueTemplate> PossibleTemplates => new IssueTemplate[]
    {
        new IssueTemplateOutOfRange(this),
        new IssueTemplateOverlapping(this),
        new IssueTemplateEmptyText(this),
    };

    protected override IEnumerable<Issue> Check(Lyric lyric)
    {
        string text = lyric.Text;
        var textTags = lyric.RubyTags;

        const TextTagsUtils.Sorting sorting = TextTagsUtils.Sorting.Asc;

        var outOfRangeTags = TextTagsUtils.FindOutOfRange(textTags, text);
        var overlappingTags = TextTagsUtils.FindOverlapping(textTags, sorting);
        var emptyTextTags = TextTagsUtils.FindEmptyText(textTags);

        foreach (var textTag in outOfRangeTags)
        {
            yield return new IssueTemplateOutOfRange(this).Create(lyric, textTag);
        }

        foreach (var textTag in overlappingTags)
        {
            yield return new IssueTemplateOverlapping(this).Create(lyric, textTag);
        }

        foreach (var textTag in emptyTextTags)
        {
            yield return new IssueTemplateEmptyText(this).Create(lyric, textTag);
        }
    }

    public abstract class IssueTemplateLyricRuby : IssueTemplate
    {
        protected IssueTemplateLyricRuby(ICheck check, IssueType type, string unformattedMessage)
            : base(check, type, unformattedMessage)
        {
        }

        public Issue Create(Lyric lyric, RubyTag textTag) => new LyricRubyTagIssue(lyric, this, textTag, textTag);
    }

    public class IssueTemplateOutOfRange : IssueTemplateLyricRuby
    {
        public IssueTemplateOutOfRange(ICheck check)
            : base(check, IssueType.Error, "Ruby tag index is out of range.")
        {
        }
    }

    public class IssueTemplateOverlapping : IssueTemplateLyricRuby
    {
        public IssueTemplateOverlapping(ICheck check)
            : base(check, IssueType.Problem, "Ruby tag index is overlapping to another ruby tag.")
        {
        }
    }

    public class IssueTemplateEmptyText : IssueTemplateLyricRuby
    {
        public IssueTemplateEmptyText(ICheck check)
            : base(check, IssueType.Problem, "Ruby tag's text should not be empty or white-space only.")
        {
        }
    }
}
