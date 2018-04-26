using System;

namespace HydraCalc.Weapon
{
    public class Weapon : IWeapon, IEquatable<Weapon>
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
            return DamageType.Cut;
        }

        private int GetGrowth(int amountOfHeads) =>
            DamageType.DoubleGrowth ? GetCut(amountOfHeads) * 2 : DamageType.Growth;

        public int GetHitDifference(int amountOfHeads)
        {
            var cut = GetCut(amountOfHeads);

            if (cut > amountOfHeads || cut == 0) return 0;
            if (amountOfHeads == cut) return cut;

            return cut - GetGrowth(amountOfHeads);
        }

        public bool Equals(Weapon other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(DamageType, other.DamageType);
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType()) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((Weapon) obj);
        }

        public override int GetHashCode() => DamageType?.GetHashCode() ?? 0;
        public static bool operator ==(Weapon left, Weapon right) => Equals(left, right);
        public static bool operator !=(Weapon left, Weapon right) => !Equals(left, right);
    }
}
