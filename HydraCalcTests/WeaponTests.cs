using HydraCalc.Weapon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HydraCalcTests
{
    [TestClass]
    public class WeaponTests
    {
        [TestMethod]
        public void GetHitDifference_CutGreaterThanHeads_Is0()
        {
            // arrange
            var expected = 0;
            var damageType = Damage.NormalCutNormalGrowth(19, 3);
            IWeapon weapon = new Weapon(damageType);

            // act
            var result = weapon.GetHitDifference(10);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetHitDifference_NoDivisor_NoGrowth_Is8()
        {
            // arrange
            var expected = 8;
            var damageType = Damage.NormalCutNormalGrowth(8, 0);
            IWeapon weapon = new Weapon(damageType);

            // act
            var result = weapon.GetHitDifference(8);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetHitDifference_NoDivisor_NormalGrowth_Is5()
        {
            // arrange
            var expected = 5;
            var damageType = Damage.NormalCutNormalGrowth(6, 1);
            IWeapon weapon = new Weapon(damageType);

            // act
            var result = weapon.GetHitDifference(15);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetHitDifference_NoDivisor_DoubleGrowth_IsNegative7()
        {
            // arrange
            var expected = -14;
            var damageType = Damage.NormalCutDoubleGrowth(14);
            IWeapon weapon = new Weapon(damageType);

            // act
            var result = weapon.GetHitDifference(25);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetHitDifference_Divisor_NoGrowth_Is10()
        {
            // arrange
            var expected = 10;
            var damageType = Damage.DivisorNormalGrowth(2, 0);
            IWeapon weapon = new Weapon(damageType);

            // act
            var result = weapon.GetHitDifference(20);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetHitDifference_Divisor_NormalGrowth_Is7()
        {
            // arrange
            var expected = 7;
            var damageType = Damage.DivisorNormalGrowth(2, 8);
            IWeapon weapon = new Weapon(damageType);

            // act
            var result = weapon.GetHitDifference(30);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetHitDifference_Divisor_DoubleGrowth_IsNegative24()
        {
            // arrange
            var expected = -24;
            var damageType = Damage.DivisorDoubleGrowth(4);
            IWeapon weapon = new Weapon(damageType);

            // act
            var result = weapon.GetHitDifference(32);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}
