using System.Collections.Generic;
using HydraCalc.Weapon;

namespace HydraCalc
{
    public interface ICalculateKill
    {
        IEnumerable<IWeapon> CalculateKill(IEnumerable<IWeapon> weapons, int amountOfHeads);
    }
}