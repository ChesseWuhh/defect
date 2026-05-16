
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using STS2RitsuLib.Scaffolding.Content;

namespace Defect.Scripts;

public class IceLancePower : TemporaryFrostFocusPower
{
    public override PowerAssetProfile AssetProfile => new(
        IconPath: "res://Defect/images/powers/ice_lance_power.png",
        BigIconPath: "res://Defect/images/powers/ice_lance_power.png"
    );

    public override AbstractModel OriginModel => ModelDb.Card<IceLance>();
}