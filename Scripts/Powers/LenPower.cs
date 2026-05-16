using System.Linq;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Keywords;
using STS2RitsuLib.Scaffolding.Content.Patches;


namespace Defect.Scripts;

public sealed class LenPower : CustomPowerTemplate
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile => new(
        IconPath: "res://Defect/images/powers/len_power.png",
        BigIconPath: "res://Defect/images/powers/len_power.png"
    );

    public decimal OnModifyOrbEvokeValue(OrbModel orb, decimal amount)
    {
        if (orb.Owner == base.Owner.Player && orb is GlassOrb glassOrb)
        {
            return amount + glassOrb.PassiveVal * base.Amount;
        }
        return amount;
    }
}

