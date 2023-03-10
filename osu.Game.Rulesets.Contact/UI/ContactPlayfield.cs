using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Utils;
using osu.Game.Rulesets.Contact.Objects.Controller;
using osu.Game.Rulesets.Contact.Objects.Drawables;
using osu.Game.Rulesets.Contact.Scenes;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Contact.UI;

[Cached]
public partial class ContactPlayfield : Playfield
{
    public const int SIZE = 50000;
    private readonly Container viewPort;

    private ControllerArea controllerArea { get; }

    public ContactPlayfield()
    {
        viewPort = new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            AutoSizeAxes = Axes.Both,
        };
        viewPort.AddRange(new Drawable[]
        {
            new ContactBorder(),
            new Scene(),
            controllerArea = new ControllerArea(),
        });

        AddInternal(viewPort);
    }

    protected override void Update()
    {
        base.Update();

        moveViewPort();
    }

    private void moveViewPort() =>
        easeTo(viewPort, -controllerArea.ControllerContainer.Position);

    private void easeTo(Drawable drawable, Vector2 destination)
    {
        double dampLength = Interpolation.Lerp(3000, 40, 0.95);

        float x = (float)Interpolation.DampContinuously(drawable.X, destination.X, dampLength, Clock.ElapsedFrameTime);
        float y = (float)Interpolation.DampContinuously(drawable.Y, destination.Y, dampLength, Clock.ElapsedFrameTime);

        drawable.Position = new Vector2(x, y);
    }
}
