namespace HydraCalc.Weapon
{
    public interface IDamage
    {
        int Cut { get; }
        int CutFactor { get; }

        int Growth { get; }
        bool DoubleGrowth { get; }
    }
}