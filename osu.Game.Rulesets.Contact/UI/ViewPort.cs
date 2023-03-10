using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Utils;
using osu.Game.Rulesets.Contact.Objects.Controller;
using osu.Game.Rulesets.Contact.Objects.Drawables;
using osu.Game.Rulesets.Contact.Scenes;
using osuTK;

namespace osu.Game.Rulesets.Contact.UI;

public partial class ContactPlayfield
{
    /// <summary>
    /// The viewport acts as a camera for the ruleset, allowing for the camera to be moved relative to the player through animations without messing with the playfield;
    /// </summary>
    public partial class ViewPort : Container
    {
        private readonly ControllerArea controllerArea;

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
                this.controllerArea
            };
        }

        protected override void Update() => moveViewPort();

        private void moveViewPort() =>
            easeTo(this, -controllerArea.ControllerContainer.Position);

        private void easeTo(Drawable drawable, Vector2 destination)
        {
            double dampLength = Interpolation.Lerp(3000, 40, 0.95);

            float x = (float)Interpolation.DampContinuously(drawable.X, destination.X, dampLength, Clock.ElapsedFrameTime);
            float y = (float)Interpolation.DampContinuously(drawable.Y, destination.Y, dampLength, Clock.ElapsedFrameTime);

            drawable.Position = new Vector2(x, y);
        }
    }
}
