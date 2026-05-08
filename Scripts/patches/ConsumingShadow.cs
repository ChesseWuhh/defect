using STS2RitsuLib.Patching.Models;
using STS2RitsuLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Defect.Scripts
{
    public class ConsumingShadowPatch : IPatchMethod
    {
        public static string PatchId => "consuming_shadow_patch";
        public static string Description => "修改吞噬暗影的机制";
        public static bool IsCritical => false;

        public static ModPatchTarget[] GetTargets() => [
            new(typeof(CardModel), "Onplay")
        ];

        public static async Task<bool> Prefix(CardModel __instance, PlayerChoiceContext context, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(__instance.Owner.Creature, "Cast", __instance.Owner.Character.CastAnimDelay);
            await PowerCmd.Apply<ConsumingShadowPower>(context, __instance.Owner.Creature, __instance.DynamicVars["ConsumingShadowPower"].BaseValue, __instance.Owner.Creature, __instance);
            return false;
        }
    }
}