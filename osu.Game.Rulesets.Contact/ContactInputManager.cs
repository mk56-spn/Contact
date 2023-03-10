// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Allocation;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Contact
{
    [Cached]
    public partial class ContactInputManager : RulesetInputManager<ContactAction>
    {
        public ContactInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum ContactAction
    {
        [Description("left")]
        Left,

        [Description("right")]
        Right,

        [Description("up")]
        Up,

        [Description("down")]
        Down
    }
}
