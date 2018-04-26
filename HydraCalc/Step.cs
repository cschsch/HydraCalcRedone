using HydraCalc.Weapon;

namespace HydraCalc
{
    internal struct Step
    {
        public IWeapon Weapon { get; }
        public int AmountOfHeads { get; }
        public int NumberOfStep { get; }

        public Step(IWeapon weapon, int amountOfHeads, int numberOfStep)
        {
            Weapon = weapon;
            AmountOfHeads = amountOfHeads;
            NumberOfStep = numberOfStep;
        }
    }
}