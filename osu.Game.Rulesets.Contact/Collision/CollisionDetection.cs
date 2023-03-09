using osu.Framework.Graphics;

namespace osu.Game.Rulesets.Contact.Collision;

public static class CollisionDetection
{
    public static bool Rectangle(Drawable rectangle1, Drawable rectangle2)
    {
        int val = 0;

        if (rectangle1.X + rectangle1.Width / 2 >= rectangle2.X - rectangle2.Width / 2 &&
            rectangle1.X - rectangle1.Width / 2 <= rectangle2.X + rectangle2.Width / 2)
        {
            val += 1;
        }

        if (rectangle1.Y + rectangle1.Height / 2 >= rectangle2.Y - rectangle2.Height / 2 &&
            rectangle1.Y - rectangle1.Height / 2 <= rectangle2.Y + rectangle2.Height / 2)
        {
            val += 1;
        }

        return val == 2;
    }
}
