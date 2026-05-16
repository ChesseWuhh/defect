using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using STS2RitsuLib;
using STS2RitsuLib.Patching.Models;

namespace Defect.Scripts;

public class IceLancePatch : IPatchMethod
{
    public static string PatchId => "ice_lance_patch";
    public static string Description => "修改冰之长枪的效果";
    public static bool IsCritical => false;

    public static ModPatchTarget[] GetTargets() => [
        new(typeof(IceLance), "OnPlay")
    ];

    public static void Postfix(IceLance __instance, PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var logger = RitsuLibFramework.CreateLogger("Defect");
        logger.Info("----------------------IceLancePatch triggered----------------------");
        if (__instance is not IceLance) return;
        var player = __instance.Owner;
        var result = PowerCmd.Apply<IceLancePower>(choiceContext, player.Creature, __instance.IsUpgraded? 3 : 2, player.Creature, __instance);
    }
}