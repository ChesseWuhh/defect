using System.Collections.Generic;
using System.Linq;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Unlocks;
using STS2RitsuLib.Patching.Models;

namespace Defect.Scripts
{
    public class CardPoolFilterPatch : IPatchMethod
    {
        public static string PatchId => "card_pool_filter_patch";
        public static string Description => "过滤掉指定的原版卡牌";
        public static bool IsCritical => false;

        // 需要排除的卡牌 ID 列表
        private static readonly HashSet<string> ExcludedCardIds = new HashSet<string>
        {
            "ConsumingShadow"
        };

        public static ModPatchTarget[] GetTargets() => new[]
        {
            new ModPatchTarget(typeof(CardPoolModel), "GetUnlockedCards")
        };

        public static void Postfix(UnlockState unlockState, ref IEnumerable<CardModel> __result)
        {
            __result = __result.Where(card => !ExcludedCardIds.Contains(card.Id.Entry))
                               .ToList();
        }
    }
}