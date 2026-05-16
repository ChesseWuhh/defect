using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Orbs;
using STS2RitsuLib.Patching.Models;

namespace Defect.Scripts
{
    public class OrbEvokePatch : IPatchMethod
    {
        public static string PatchId => "orb_evoke_patch";
        public static string Description => "修改充能球的激发效果";
        public static bool IsCritical => false;

        // 补丁基类 OrbModel 的 Evoke 方法
        public static ModPatchTarget[] GetTargets() => [
            new(typeof(GlassOrb), "get_EvokeVal"),
            new(typeof(LightningOrb), "get_EvokeVal"),
            new(typeof(FrostOrb), "get_EvokeVal"),
            new(typeof(DarkOrb), "get_EvokeVal"),
            new(typeof(PlasmaOrb), "get_EvokeVal")
        ];

        public static void Postfix(OrbModel __instance, ref decimal __result)
        {
            var value = __result;
            __result = CustomHook.ModifyOrbEvokeValue(__instance, value);
        }
    }
}