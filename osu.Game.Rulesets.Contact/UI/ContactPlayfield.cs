using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Utils;
using osu.Game.Rulesets.Contact.Objects.Controller;
using osu.Game.Rulesets.Contact.Scenes;
using osu.Game.Rulesets.UI;
using osuTK;
using osu.Game.Rulesets.Contact.Objects.Shapes;

namespace osu.Game.Rulesets.Contact.UI;

[Cached]
public partial class ContactPlayfield : Playfield
{
    public const int SIZE = 500000;
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
            new Border(),
            new Scene(),
            controllerArea = new ControllerArea(),
        });

        AddInternal(viewPort);

        AddInternal(new TestCirc
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Size = new Vector2(20),
        });
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

    private partial class Border : Container
    {
        private const int border_thickness = 5;

        public Border()
        {
            Depth = float.MaxValue;
            Colour = Colour4.White;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Masking = true;
            MaskingSmoothness = 0;
            BorderColour = Colour4.Red;
            BorderThickness = border_thickness;
            Size = new Vector2(SIZE + border_thickness * 2);
            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = new ColourInfo
                {
                    TopLeft = Colour4.YellowGreen,
                    TopRight = Colour4.Crimson,
                    BottomLeft = Colour4.Aquamarine.Darken(0.3f),
                    BottomRight = Colour4.MediumPurple,
                }
            };
        }
    }
}
