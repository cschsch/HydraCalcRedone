namespace HydraCalc.Weapon
{
    public struct Damage : IDamage
    {
        public int Cut { get; }
        public int CutFactor { get; }
        public int Growth { get; }
        public bool DoubleGrowth { get; }

        private Damage(int cut, int cutFactor, int growth, bool doubleGrowth)
        {
            Cut = cut;
            CutFactor = cutFactor;
            Growth = growth;
            DoubleGrowth = doubleGrowth;
        }

        public static IDamage NormalCutNormalGrowth(int cut, int growth) => new Damage(cut, 1, growth, false);
        public static IDamage NormalCutDoubleGrowth(int cut) => new Damage(cut, 1, 0, true);
        public static IDamage DivisorNormalGrowth(int cutFactor, int growth) => new Damage(0, cutFactor, growth, false);
        public static IDamage DivisorDoubleGrowth(int cutFactor) => new Damage(0, cutFactor, 0, true);
    }
}