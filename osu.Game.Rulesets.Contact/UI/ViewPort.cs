using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Utils;
using osu.Game.Rulesets.Contact.Objects.Controller;
using osu.Game.Rulesets.Contact.Objects.Drawables;
using osu.Game.Rulesets.Contact.Scenes;
using osuTK;

namespace osu.Game.Rulesets.Contact.UI;

/// <summary>
/// The viewport acts as a camera for the ruleset, allowing for the camera to be moved relative to the player through animations without messing with the playfield;
/// </summary>
public partial class ViewPort : Container
{
    private readonly ControllerArea controllerArea;
    private readonly List<Box> boxes = new();

    public bool CollisionColours = false;

    public ViewPort(ControllerArea controllerArea)
    {
        this.controllerArea = controllerArea;
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        AutoSizeAxes = Axes.Both;

        Children = new Drawable[]
        {
            new ContactBorder(),
            new Scene(),
            this.controllerArea,
        };

        const int amount = ContactPlayfield.SIZE / 200;

        foreach (int _ in Enumerable.Range(0, 2000))
        {
            boxes.Add(new Box
                {
                    Size = new Vector2(RNG.NextSingle(20, 100)),
                    Position = new Vector2(RNG.Next(-amount, amount), RNG.Next(-amount, amount)),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            );
        }

        AddRangeInternal(boxes);
    }

    protected override void UpdateAfterChildren()
    {
        base.UpdateAfterChildren();
        moveViewPort();

        foreach (var box in Children.OfType<Box>())
        {
            if (box.BoundingBox.IntersectsWith(controllerArea.Controller.BoundingBox))
            {
                // The rectangles intersect. Calculate the minimum distance needed to move them apart.
                var intersection = RectangleF.Intersect(box.BoundingBox, controllerArea.Controller.BoundingBox);

                // Move the rectangles apart by changing their positions.
                controllerArea.Controller.Position = box.Position + new Vector2(0, -box.Height / 2f);
            }

            float distance = Vector2.Distance(controllerArea.Controller.Position, box.Position);

            Colour4 hslCOl = Colour4.FromHSL(0, 0, 3 / (1 + distance / 20));

            box.Colour = hslCOl * controllerArea.Controller.Colour.TopLeft.Linear.ToLinear();
        }

        controllerArea.Controller.EdgeEffect = new EdgeEffectParameters
        {
            Radius = 500,
            Roundness = 50,
            Type = EdgeEffectType.Glow,
            Colour = controllerArea.Controller.Colour.TopLeft.Linear.Opacity(0.25f),
        };

        controllerArea.Controller.Rotation = (float)(controllerArea.Controller.Rotation + Clock.ElapsedFrameTime / 1000 * 0);
    }

    private void onCollide(Box box)
    {
        // Calculate the direction in which to push rectangle2 away from rectangle1
        Vector2 direction = (box.Position - controllerArea.Controller.Position).Normalized();

        // Push rectangle2 away from rectangle1 in the calculated direction
        float pushDistance = (controllerArea.Controller.Size.X + box.Size.X) / 2 - (box.Position - controllerArea.Controller.Position).Length;
        // Push rectangle2 away from rectangle1 in the calculated direction and distance
        box.Position = box.Position + direction * pushDistance;
    }

    private void moveViewPort() =>
        easeTo(this, -controllerArea.Controller.Position);

    private void easeTo(Drawable drawable, Vector2 destination)
    {
        double dampLength = Interpolation.Lerp(3000, 40, 0.95);

        float x = (float)Interpolation.DampContinuously(drawable.X, destination.X, dampLength, Clock.ElapsedFrameTime);
        float y = (float)Interpolation.DampContinuously(drawable.Y, destination.Y, dampLength, Clock.ElapsedFrameTime);

        drawable.Position = new Vector2(x, y);
    }
}
