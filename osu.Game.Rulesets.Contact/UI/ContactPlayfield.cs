using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Contact.Objects.Controller;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Contact.UI;

[Cached]
public partial class ContactPlayfield : Playfield
{
    public const int SIZE = Controller.CONTROLLER_SIZE * 1000;
    public ViewPort ViewPort { get; }
    public ControllerArea ControllerArea { get; }

    public ContactPlayfield()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        ControllerArea = new ControllerArea();
        AddInternal(ViewPort = new ViewPort());
    }
}
