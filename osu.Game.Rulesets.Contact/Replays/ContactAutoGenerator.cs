// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Contact.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Contact.Replays
{
    public class ContactAutoGenerator : AutoGenerator<ContactReplayFrame>
    {
        public new Beatmap<ContactHitObject> Beatmap => (Beatmap<ContactHitObject>)base.Beatmap;

        public ContactAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        protected override void GenerateFrames()
        {
            Frames.Add(new ContactReplayFrame());

            foreach (ContactHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new ContactReplayFrame
                {
                    Time = hitObject.StartTime,
                    Position = hitObject.Position,
                    // todo: add required inputs and extra frames.
                });
            }
        }
    }
}
