using System.Linq;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

using STS2RitsuLib.Scaffolding.Content;
using MegaCrit.Sts2.Core.Entities.Players;

namespace Defect.Scripts;

public sealed class PanicPower : ModPowerTemplate
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override PowerAssetProfile AssetProfile => new(
        IconPath: "res://Defect/images/powers/panic_power.png",
        BigIconPath: "res://Defect/images/powers/panic_power.png"
    );

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? __)
	{
        var player = base.Owner.Player;
		if (player != null && target == base.Owner && dealer != null && result.BlockedDamage > 0 && props.IsPoweredAttack())
		{
			var orbs = player.PlayerCombatState?.OrbQueue?.Orbs;
            var orb = orbs?.Count > 0 ? orbs[0] : null;
            if (orb != null)
            {
                await OrbCmd.Passive(choiceContext, orb, null);
            }
		}
	}
}
