using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics.Containers;
using osuTK;

namespace osu.Game.Rulesets.Contact.Objects.Controller;

public partial class Controller : BeatSyncedContainer
{
    public const int CONTROLLER_SIZE = 100;

    public Controller()
    {
        Masking = true;
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        Child = new Box
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
        };
        Size = new Vector2(CONTROLLER_SIZE);
    }
}
