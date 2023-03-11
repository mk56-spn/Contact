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

        var newPos = Controller.Position + new Vector2(xSpeed, ySpeed) * new Vector2((float)(Clock.ElapsedFrameTime / 1000f));

        if (HorizontalCheck == 0)
        {
            newPos.X = Controller.Position.X;
        }

        if (HorizontalCheck != 0 || VerticalCheck != 0)
        {
            Controller.FadeColour(Colour4.Blue, 300);
        }
        else
        {
            Controller.FadeColour(Colour4.White, 300);
        }

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
