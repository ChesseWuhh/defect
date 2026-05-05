using System.Reflection;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using STS2RitsuLib;
using STS2RitsuLib.Interop;
using STS2RitsuLib.Patching.Core;

namespace Defect.Scripts;

[ModInitializer(nameof(Init))]
public class Entry
{
    // 你的modid
    public const string ModId = "Defect";
    public static readonly Logger Logger = RitsuLibFramework.CreateLogger(ModId);

    public static void Init()
    {
        var assembly = Assembly.GetExecutingAssembly();
        RitsuLibFramework.EnsureGodotScriptsRegistered(assembly, Logger);
        
        RitsuLibFramework.CreateContentPack("defect")
            .Card<DefectCardPool, Len>()
            .Power<LenPower>()
            .Apply();

        var patcher = RitsuLibFramework.CreatePatcher("DefectPatch", "core");
        patcher.RegisterPatch<GlassworkRarityPatch>();
        
        if (!patcher.PatchAll())
            throw new InvalidOperationException("Required patches failed.");
        
        Logger.Info("Defect mod initialized successfully.");

    }
}