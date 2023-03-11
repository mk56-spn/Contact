using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Contact.Tests;

public partial class ContactTestScene : PlayerTestScene
{
    protected override Ruleset CreatePlayerRuleset() =>
        new ContactRuleset();
}
