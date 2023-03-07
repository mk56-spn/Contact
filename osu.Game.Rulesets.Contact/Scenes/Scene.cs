using osu.Game.Rulesets.Contact.UI;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Contact.Scenes;

/// <summary>
/// The scene can be thought of as a being akin to an "OsuScreen" but for gameplay. it provide a structure into which layers are nested
/// </summary>
public partial class Scene : HitObjectContainer
{
    public Scene()
    {
        Masking = true;
        Size = new Vector2(ContactPlayfield.SIZE);
    }
}
