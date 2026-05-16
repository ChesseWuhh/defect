using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Orbs;
using STS2RitsuLib;
using STS2RitsuLib.Scaffolding.Content;

namespace Defect.Scripts;

public sealed class IntoShadowPower : ModPowerTemplate
{
	public override PowerType Type => PowerType.Buff;

	public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile => new(
        IconPath: "res://Defect/images/powers/into_shadow_power.png",
        BigIconPath: "res://Defect/images/powers/into_shadow_power.png"
    );

	public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
	{
		if (side == base.Owner.Side)
                {
                    var player = base.Owner.Player;
                    if (player != null && player.PlayerCombatState?.OrbQueue.Orbs.Count > 0)
                    {
                        var q = player.PlayerCombatState?.OrbQueue;
                        if (q == null) return;
                        int amount = base.Amount;
                        int count = 0;
                        if (q.Orbs == null || q.Orbs[0] == null) return;

                        if (q.Orbs[0] is DarkOrb)
                        {
                              await CreatureCmd.TriggerAnim(player.Creature, "Cast", player.Character.CastAnimDelay);
                        }    

                        for (int i = 0; i < q.Orbs.Count && count < amount; i++)
                        {
                            var orb = q.Orbs[i];
                            if (orb is not DarkOrb)
                            {
                                var newOrb = ModelDb.Orb<DarkOrb>().ToMutable();
                                newOrb.Owner = player;                             
                                newOrb.PlayChannelSfx();
                                await OrbCmd.Replace(orb, newOrb, player);
                                await Cmd.Wait(0.1f);                        
                            }
                            await OrbCmd.Passive(choiceContext, q.Orbs[i], null);
                            await Cmd.Wait(0.1f);
                            count++;
                        }
                    }
                }
	}
}
