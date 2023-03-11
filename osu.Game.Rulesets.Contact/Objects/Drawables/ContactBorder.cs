using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Contact.UI;
using osuTK;

namespace osu.Game.Rulesets.Contact.Objects.Drawables;

public partial class ContactBorder : Container
{
    private const int border_thickness = 5;

    public ContactBorder()
    {
        Depth = float.MaxValue;
        Colour = Colour4.White;
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        Masking = true;
        MaskingSmoothness = 0;
        BorderColour = Colour4.Red;
        BorderThickness = border_thickness;
        Size = new Vector2(ContactPlayfield.SIZE + border_thickness * 2);
        Child = new Box
        {
            RelativeSizeAxes = Axes.Both,
            Colour = Colour4.Black
        };
    }
}
