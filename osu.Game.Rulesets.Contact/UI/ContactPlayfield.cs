using osu.Framework.Allocation;
using osu.Game.Rulesets.Contact.Objects.Controller;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Contact.UI;

[Cached]
public partial class ContactPlayfield : Playfield
{
    public const int SIZE = 50000;
    public ViewPort viewPort { get; }

    private ControllerArea controllerArea { get; }

    public ContactPlayfield()
    {
        AddInternal(viewPort = new ViewPort(controllerArea = new ControllerArea()));
    }
}
