﻿// Copyright (c) andy840119 <andy840119@gmail.com>. Licensed under the GPL Licence.
// See the LICENCE file in the repository root for full licence text.

using Newtonsoft.Json;
using osu.Framework.Bindables;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects.Types;
using osu.Game.Rulesets.Karaoke.Objects.Workings;

namespace osu.Game.Rulesets.Karaoke.Objects;

/// <summary>
/// Placing the properties that set by <see cref="KaraokeBeatmapProcessor"/> or being calculated.
/// Those properties will not be saved into the beatmap.
/// </summary>
public partial class Lyric : IHasWorkingProperty<LyricWorkingProperty>
{
    [JsonIgnore]
    private readonly LyricWorkingPropertyValidator workingPropertyValidator;

    public bool InvalidateWorkingProperty(LyricWorkingProperty workingProperty)
        => workingPropertyValidator.Invalidate(workingProperty);

    private void validateWorkingProperty(LyricWorkingProperty workingProperty)
        => workingPropertyValidator.Validate(workingProperty);

    public LyricWorkingProperty[] GetAllInvalidWorkingProperties()
        => workingPropertyValidator.GetAllInvalidFlags();

    [JsonIgnore]
    public double LyricStartTime { get; private set; }

    [JsonIgnore]
    public double LyricEndTime { get; private set; }

    [JsonIgnore]
    public double LyricDuration => LyricEndTime - LyricStartTime;

    /// <summary>
    /// Lyric's start time is created from <see cref="KaraokeBeatmapProcessor"/> and should not be saved.
    /// </summary>
    [JsonIgnore]
    public override double StartTime
    {
        get => base.StartTime;
        set
        {
            base.StartTime = value;
            validateWorkingProperty(LyricWorkingProperty.StartTime);
        }
    }

    [JsonIgnore]
    public readonly Bindable<double> DurationBindable = new BindableDouble();

    /// <summary>
    /// Lyric's duration is created from <see cref="KaraokeBeatmapProcessor"/> and should not be saved.
    /// </summary>
    [JsonIgnore]
    public double Duration
    {
        get => DurationBindable.Value;
        set
        {
            DurationBindable.Value = value;
            validateWorkingProperty(LyricWorkingProperty.Duration);
        }
    }

    /// <summary>
    /// The time at which the HitObject end.
    /// </summary>
    [JsonIgnore]
    public double EndTime => StartTime + Duration;

    [JsonIgnore]
    public readonly Bindable<int?> PageIndexBindable = new();

    /// <summary>
    /// Order
    /// </summary>
    [JsonIgnore]
    public int? PageIndex
    {
        get => PageIndexBindable.Value;
        set
        {
            PageIndexBindable.Value = value;
            validateWorkingProperty(LyricWorkingProperty.Page);
        }
    }

    [JsonIgnore]
    public readonly Bindable<Lyric?> ReferenceLyricBindable = new();

    /// <summary>
    /// Reference lyric.
    /// Link the same or similar lyric for reference or sync the properties.
    /// </summary>
    [JsonIgnore]
    public Lyric? ReferenceLyric
    {
        get => ReferenceLyricBindable.Value;
        set
        {
            ReferenceLyricBindable.Value = value;
            validateWorkingProperty(LyricWorkingProperty.ReferenceLyric);
        }
    }
}
