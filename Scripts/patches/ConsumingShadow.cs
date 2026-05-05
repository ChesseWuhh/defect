using STS2RitsuLib.Patching.Models;
using STS2RitsuLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Commands;

namespace Defect.Scripts
{
    public class ConsumingShadowPatch : IPatchMethod
    {
        public static string PatchId => "consuming_shadow_patch";
        public static string Description => "修改吞噬暗影的机制";
        public static bool IsCritical => false;

        public static ModPatchTarget[] GetTargets() => [
            new(typeof(PowerModel), "AfterTurnEnd")
        ];

        public static async Task<bool> Prefix(PowerModel __instance, PlayerChoiceContext context, CombatSide side)
        {
            if (side == __instance.Owner.Side)
            {
                var player = __instance.Owner.Player;
                if (player != null && player.PlayerCombatState?.OrbQueue.Orbs.Count > 0)
                {
                    var orbs = player.PlayerCombatState.OrbQueue.Orbs;
                    for (var i = 0; i < orbs.Count; i++)
                    {
                        if (orbs[i] is not DarkOrb)
                        {
                            var orb = orbs[i];
                            if (orb == null) continue;
                            player.PlayerCombatState.OrbQueue.Remove(orb);
                            player.PlayerCombatState.OrbQueue.Insert(i, new DarkOrb());                           
                        }
                        await OrbCmd.Passive(context, orbs[i], null);
                    }
                }
            }
            return false;
        }
    }
}