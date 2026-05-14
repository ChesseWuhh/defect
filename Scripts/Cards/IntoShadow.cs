using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2RitsuLib.Scaffolding.Content;

namespace Defect.Scripts;

public sealed class IntoShadow : ModCardTemplate
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new PowerVar<IntoShadowPower>(1m)
    ];

	public IntoShadow()
		: base(2, CardType.Power, CardRarity.Rare, TargetType.Self)
	{
	}

    public override CardAssetProfile AssetProfile => new(
        PortraitPath: $"res://Defect/images/cards/into_shadow.png"
    );

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		await PowerCmd.Apply<IntoShadowPower>(choiceContext, base.Owner.Creature, base.DynamicVars["IntoShadowPower"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["IntoShadowPower"].UpgradeValueBy(1m);
	}
}
