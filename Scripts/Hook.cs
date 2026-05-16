
using MegaCrit.Sts2.Core.Models;

namespace Defect.Scripts;
public class CustomHook
{
    public static decimal ModifyOrbEvokeValue(OrbModel orb, decimal originalValue)
    {
        decimal result = originalValue;
        var state = orb.CombatState;
        foreach (var listener in state.IterateHookListeners())
        {
            if (listener is CustomPowerTemplate power)
            {
                result = power.ModifyOrbEvokeValue(orb, result);
            }
        }
        return result;
    }
    
}