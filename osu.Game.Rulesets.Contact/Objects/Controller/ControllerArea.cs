using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Contact.UI;
using osuTK;

namespace osu.Game.Rulesets.Contact.Objects.Controller;

public partial class ControllerArea : Container, IKeyBindingHandler<ContactAction>
{
    public Controller Controller { get; }

    public int HorizontalCheck;
    public int VerticalCheck;

    public ControllerArea()
    {
        Masking = true;
        BorderThickness = 3;
        MaskingSmoothness = 10;
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        Size = new Vector2(ContactPlayfield.SIZE);
        Depth = float.MinValue;

        AddRange(new Drawable[]
        {
            Controller = new Controller(),
        });
    }

    [BackgroundDependencyLoader]
    private void load(ContactPlayfield contactPlayfield)
    {
        Add(contactPlayfield.HitObjectContainer);
    }

    public bool OnPressed(KeyBindingPressEvent<ContactAction> e)
    {
        switch (e.Action)
        {
            case ContactAction.Left:
                HorizontalCheck--;
                break;

            case ContactAction.Right:
                HorizontalCheck++;
                break;

            case ContactAction.Up:
                VerticalCheck--;
                break;

            case ContactAction.Down:
                VerticalCheck++;
                break;
        }

        return false;
    }

    public void OnReleased(KeyBindingReleaseEvent<ContactAction> e)
    {
        switch (e.Action)
        {
            case ContactAction.Left or ContactAction.Right:
                HorizontalCheck = e.Action == ContactAction.Left ? HorizontalCheck + 1 : HorizontalCheck - 1;
                break;

            case ContactAction.Up or ContactAction.Down:
                VerticalCheck = e.Action == ContactAction.Up ? VerticalCheck + 1 : VerticalCheck - 1;
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

        var newPos = Controller.Position + new Vector2(xSpeed, ySpeed) * new Vector2((float)(Clock.ElapsedFrameTime / 1000f));

        Controller.FadeColour(HorizontalCheck != 0 || VerticalCheck != 0 ? Colour4.Blue : Colour4.White, 300);

        Controller.MoveTo(newPos);

        clampToPlayfield();

        base.Update();
    }

    private void clampToPlayfield()
    {
        Controller.MoveToX(Math.Clamp(Controller.Position.X, -ContactPlayfield.SIZE / 2f + Controller.Width / 2, ContactPlayfield.SIZE / 2f - Controller.Width / 2));
        Controller.MoveToY(Math.Clamp(Controller.Position.Y, -ContactPlayfield.SIZE / 2f + Controller.Width / 2, ContactPlayfield.SIZE / 2f - Controller.Width / 2));
    }
}
