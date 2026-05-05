using STS2RitsuLib.Patching.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using System.Reflection;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib;
using MegaCrit.Sts2.Core.Models;

namespace Defect.Scripts
{
    public class GlassworkRarityPatch : IPatchMethod
    {
        public static string PatchId => "glasswork_rarity_patch";
        public static string Description => "修改 Glasswork 的稀有度为普通";
        public static bool IsCritical => false;

        // 补丁基类 CardModel 的 get_Rarity 方法
        public static ModPatchTarget[] GetTargets() => [
            new(typeof(CardModel), "get_Rarity")
        ];

        public static void Postfix(CardModel __instance, ref CardRarity __result)
        {
            // 只修改 Glasswork 类型的卡牌
            if (__instance is Glasswork)
            {
                __result = CardRarity.Common;
            }
        }
    }
}