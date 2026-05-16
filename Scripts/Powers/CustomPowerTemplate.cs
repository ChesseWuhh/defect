using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Scaffolding.Content;

namespace Defect.Scripts;
public abstract class CustomPowerTemplate : ModPowerTemplate
{
    public virtual decimal ModifyOrbEvokeValue(OrbModel orb, decimal amount)
    {
        return amount;
    }
    
}