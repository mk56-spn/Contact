﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.Contact.Replays
{
    public class ContactReplayFrame : ReplayFrame
    {
        public List<ContactAction> Actions = new List<ContactAction>();
        public Vector2 Position;

        public ContactReplayFrame(ContactAction? button = null)
        {
            if (button.HasValue)
                Actions.Add(button.Value);
        }
    }
}
