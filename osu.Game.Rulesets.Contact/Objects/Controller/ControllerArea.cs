using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osu.Game.Rulesets.Contact.UI;
using osuTK;

namespace osu.Game.Rulesets.Contact.Objects.Controller;

public partial class ControllerArea : Container, IKeyBindingHandler<ContactAction>
{
    private readonly ControllerContainer controllerContainer;

    public int HorizontalCheck;
    public int VerticalCheck;

    public ControllerArea()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        Size = new Vector2(ContactPlayfield.SIZE);

        AddRange(new Drawable[]
        {
            controllerContainer = new ControllerContainer(),

            new Container
            {
                Colour = Colour4.White,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Masking = true,
                MaskingSmoothness = 0,
                BorderColour = Colour4.Red,
                BorderThickness = 5,
                Size = new Vector2(10010),
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Transparent
                }
            }
        });
    }

    public bool OnPressed(KeyBindingPressEvent<ContactAction> e)
    {
        switch (e.Action)
        {
            case ContactAction.Button1:
                HorizontalCheck--;
                break;

            case ContactAction.Button2:
                HorizontalCheck++;
                break;

            case ContactAction.Button3:
                VerticalCheck--;
                break;

            case ContactAction.Button4:
                VerticalCheck++;
                break;
        }

        return false;
    }

    public void OnReleased(KeyBindingReleaseEvent<ContactAction> e)
    {
        switch (e.Action)
        {
            case ContactAction.Button1 or ContactAction.Button2:
                HorizontalCheck = e.Action == ContactAction.Button1 ? HorizontalCheck + 1 : HorizontalCheck - 1;
                break;

            case ContactAction.Button3 or ContactAction.Button4:
                VerticalCheck = e.Action == ContactAction.Button3 ? VerticalCheck + 1 : VerticalCheck - 1;
                break;
        }
    }

    protected override void Update()
    {
        float xSpeed = 0;
        float ySpeed = 0;

        const int speed = 1000;

        xSpeed = HorizontalCheck switch
        {
            -1 => -speed,
            1 => speed,
            _ => xSpeed
        };

        ySpeed = VerticalCheck switch
        {
            -1 => -speed,
            1 => speed,
            _ => ySpeed
        };

        var newPos = controllerContainer.Position + new Vector2(xSpeed, ySpeed) * new Vector2((float)(Clock.ElapsedFrameTime / 1000f));

        if (HorizontalCheck == 0)
        {
            newPos.X = controllerContainer.Position.X;
        }

        controllerContainer.MoveTo(newPos);

        clampToPlayfield();
        moveViewPort();

        base.Update();
    }

    private void clampToPlayfield()
    {
        controllerContainer.MoveToX(Math.Clamp(controllerContainer.Position.X, -ContactPlayfield.SIZE / 2f + controllerContainer.Width / 2, ContactPlayfield.SIZE / 2f - controllerContainer.Width / 2));
        controllerContainer.MoveToY(Math.Clamp(controllerContainer.Position.Y, -ContactPlayfield.SIZE / 2f + controllerContainer.Width / 2, ContactPlayfield.SIZE / 2f - controllerContainer.Width / 2));
    }

    private void moveViewPort() =>
        easeTo(this, -controllerContainer.Position);

    private void easeTo(Drawable drawable, Vector2 destination)
    {
        double dampLength = Interpolation.Lerp(3000, 40, 0.95);

        float x = (float)Interpolation.DampContinuously(drawable.X, destination.X, dampLength, Clock.ElapsedFrameTime);
        float y = (float)Interpolation.DampContinuously(drawable.Y, destination.Y, dampLength, Clock.ElapsedFrameTime);

        drawable.Position = new Vector2(x, y);
    }
}
