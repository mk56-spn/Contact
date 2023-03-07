// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Contact.Objects.Controller;

namespace osu.Game.Rulesets.Contact.UI
{
    [Cached]
    public partial class ContactPlayfield : Playfield
    {
        public const int SIZE = 10000;

        public ControllerArea ControllerArea { get; private set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            ControllerArea = new ControllerArea();
            AddInternal(ControllerArea);
        }
    }
}
