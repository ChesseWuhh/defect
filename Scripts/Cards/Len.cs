using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Cards.DynamicVars;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Keywords;
using STS2RitsuLib.Scaffolding.Content;


namespace Defect.Scripts;

public class Len : ModCardTemplate{
    public Len() : base(
        baseCost: 1,
        type: CardType.Skill,
        rarity: CardRarity.Uncommon,
        target: TargetType.Self)
    {
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<LenPower>(1m)];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<LenPower>(choiceContext,base.Owner.Creature, base.DynamicVars["LenPower"].BaseValue, base.Owner.Creature, this);
        await OrbCmd.Channel<GlassOrb>(choiceContext, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["LenPower"].UpgradeValueBy(1m);
    }

    public override CardAssetProfile AssetProfile => new(
        PortraitPath: $"res://Defect/images/cards/len.png"
    );
}
