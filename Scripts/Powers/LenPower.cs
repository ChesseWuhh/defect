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

public sealed class LenPower : ModPowerTemplate
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile => new(
        IconPath: "res://Defect/images/powers/len_power.png",
        BigIconPath: "res://Defect/images/powers/len_power.png"
    );

    public override async Task AfterOrbEvoked(PlayerChoiceContext choiceContext, OrbModel orb, IEnumerable<Creature> targets)
    {
        if (orb.Owner == base.Owner.Player && orb is GlassOrb)
        {
            List<Creature> livingTargets = targets.Where((Creature c) => c.IsAlive).ToList();
            Flash();
            SfxCmd.Play("slash_attack.mp3");
            VfxCmd.PlayOnCreatureCenters(livingTargets, "vfx/vfx_attack_slash");
            await CreatureCmd.Damage(choiceContext, livingTargets, base.Amount * orb.PassiveVal, ValueProp.Unpowered, base.Owner, null);
        }
    }
}
