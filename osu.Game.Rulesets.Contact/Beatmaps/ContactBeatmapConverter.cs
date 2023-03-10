// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Threading;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Contact.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osuTK;

namespace osu.Game.Rulesets.Contact.Beatmaps
{
    public class ContactBeatmapConverter : BeatmapConverter<ContactHitObject>
    {
        public ContactBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        // todo: Check for conversion types that should be supported (ie. Beatmap.HitObjects.Any(h => h is IHasXPosition))
        // https://github.com/ppy/osu/tree/master/osu.Game/Rulesets/Objects/Types
        public override bool CanConvert() => true;

        protected override IEnumerable<ContactHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            yield return new ContactHitObject
            {
                Samples = original.Samples,
                StartTime = 0,
                Position = (original as IHasPosition)?.Position * 100 ?? Vector2.Zero,
            };
        }
    }
}
