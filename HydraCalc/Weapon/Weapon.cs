namespace HydraCalc.Weapon
{
    public class Weapon : IWeapon
    {
        private IDamage DamageType { get; }
        private bool IsDivisor => DamageType.CutFactor != 1;

        public Weapon(IDamage damageType)
        {
            DamageType = damageType;
        }

        private int GetCut(int amountOfHeads)
        {
            if (!IsDivisor) return DamageType.Cut;
            if (amountOfHeads % DamageType.CutFactor == 0) return amountOfHeads - (amountOfHeads / DamageType.CutFactor);
            return DamageType.Cut; // Is 0 for Divisors
        }

        private int GetGrowth(int amountOfHeads) =>
            DamageType.DoubleGrowth ? GetCut(amountOfHeads) * 2 : DamageType.Growth;

        public int GetHitDifference(int amountOfHeads)
        {
            var cut = GetCut(amountOfHeads);

            if (cut > amountOfHeads || cut == 0) return 0; // Signal that hitting the Hydra is impossible
            if (amountOfHeads == cut) return cut; // Signal that Hydra can be killed

            return cut - GetGrowth(amountOfHeads);
        }
    }
}
