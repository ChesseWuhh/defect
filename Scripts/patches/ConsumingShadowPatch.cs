using STS2RitsuLib.Patching.Models;
using STS2RitsuLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Cards;
using STS2RitsuLib.Patching.Core;
using MegaCrit.Sts2.Core.Nodes.Screens.CardLibrary;

namespace Defect.Scripts
{
    class ConsumingShadowPatch : IPatchMethod
    {
        public static string PatchId => "consuming_shadow_patch";
        public static string Description => "修改吞噬暗影的机制";
        public static bool IsCritical => false;

        public static ModPatchTarget[] GetTargets() => [
            new(typeof(NCardLibrary), "GetCard")
        ];

        public static bool Prefix(string cardId, ref CardModel __result)
        {
            return true;
        }
    }

}