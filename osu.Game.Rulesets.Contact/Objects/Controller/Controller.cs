using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace osu.Game.Rulesets.Contact.Objects.Controller;

public partial class ControllerContainer : Container
{
    public const int CONTROLLER_SIZE = 100;

    public ControllerContainer()
    {
        CornerRadius = 20;
        Masking = true;
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;

        Add(new Box
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
        });
        Size = new Vector2(CONTROLLER_SIZE);
    }
}
