// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Contact.Objects;
using osu.Game.Rulesets.Contact.Objects.Drawables;
using osu.Game.Rulesets.Contact.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Contact.UI
{
    [Cached]
    public partial class DrawableContactRuleset : DrawableRuleset<ContactHitObject>
    {
        public DrawableContactRuleset(ContactRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override Playfield CreatePlayfield() => new ContactPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new ContactFramedReplayInputHandler(replay);

        public override DrawableHitObject<ContactHitObject> CreateDrawableRepresentation(ContactHitObject h) => new DrawableContactHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new ContactInputManager(Ruleset.RulesetInfo);
    }
}
