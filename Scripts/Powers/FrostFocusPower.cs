
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Orbs;
using STS2RitsuLib.Scaffolding.Content;

namespace Defect.Scripts;

public class FrostFocusPower : ModPowerTemplate
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile => new(
        IconPath: "res://Defect/images/powers/frost_focus_power.png",
        BigIconPath: "res://Defect/images/powers/frost_focus_power.png"
    );


    public override decimal ModifyOrbValue(OrbModel orb, decimal amount)
    {
        if (orb.Owner == base.Owner.Player && orb is FrostOrb frostOrb)
        {
            return amount + base.Amount;
        }
        return amount;
    }

}