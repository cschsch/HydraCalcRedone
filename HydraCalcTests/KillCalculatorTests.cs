using System;
using System.Linq;
using HydraCalc;
using HydraCalc.Weapon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HydraCalcTests
{
    [TestClass]
    public class KillCalculatorTests
    {
        [TestMethod]
        public void KillCalculator_MaxAmountOfSteps100_ThrowsArgumentOutOfRangeException()
        {
            // arrange

            // act
            object GetKillCalculator() => new KillCalculator(100);

            // assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(GetKillCalculator);
        }

        [TestMethod]
        public void KillCalculator_MaxAmountOfSteps0_ThrowsArgumentOutOfRangeException()
        {
            // arrange

            // act
            object GetKillCalculator() => new KillCalculator(0);

            // assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(GetKillCalculator);
        }

        [TestMethod]
        public void CalculateKill_WeaponsNull_ThrowsArgumentNullException()
        {
            // arrange
            var calculator = new KillCalculator(20);

            // act
            object GetKill() => calculator.CalculateKill(null, 15);

            // assert
            Assert.ThrowsException<ArgumentNullException>(GetKill);
        }

        [TestMethod]
        public void CalculateKill_WeaponsEmpty_ThrowsArgumentException()
        {
            // arrange
            var calculator = new KillCalculator(20);

            // act
            object GetKill() => calculator.CalculateKill(Enumerable.Empty<IWeapon>(), 1);

            // assert
            Assert.ThrowsException<ArgumentException>(GetKill);
        }

        [TestMethod]
        public void CalculateKill_AmountOfHeads0_ThrowsArgumentOutOfRangeException()
        {
            // arrange
            var weapons = new[] {new Weapon(Damage.NormalCutDoubleGrowth(1))};
            var calculator = new KillCalculator(20);

            // act
            object GetKill() => calculator.CalculateKill(weapons, 0);

            // assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(GetKill);
        }

        [TestMethod]
        public void CalculateKill_NotPossible_Cut0_IsEmpty()
        {
            // arrange
            var weapons = new[] { new Weapon(Damage.NormalCutNormalGrowth(1, 1)) };
            var expected = new Weapon[] { };
            var calculator = new KillCalculator(5);

            // act
            var result = calculator.CalculateKill(weapons, 2).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateKill_NotPossible_CutGrowDifferenceLessThan0_IsEmpty()
        {
            // arrange
            var weapons = new[] {new Weapon(Damage.NormalCutDoubleGrowth(1))};
            var expected = new Weapon[] { };
            var calculator = new KillCalculator(5);

            // act
            var result = calculator.CalculateKill(weapons, 2).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateKill_OneHit_IsExpected()
        {
            // arrange
            var expected = new[] {new Weapon(Damage.NormalCutNormalGrowth(4, 0))};
            var calculator = new KillCalculator(20);

            // act
            var result = calculator.CalculateKill(expected, 4).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateKill_MultipleWeapons_OneHit_IsExpected()
        {
            // arrange
            var weapons = new[]
            {
                new Weapon(Damage.DivisorDoubleGrowth(6)),
                new Weapon(Damage.NormalCutDoubleGrowth(6)),
                new Weapon(Damage.NormalCutNormalGrowth(1, 9))
            };
            var expected = new[] {weapons[2]};
            var calculator = new KillCalculator(20);

            // act
            var result = calculator.CalculateKill(weapons, 1).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateKill_MultipleWeapons_MultipleHits_IsExpected()
        {
            // arrange
            var weapons = new[]
            {
                new Weapon(Damage.NormalCutDoubleGrowth(2)),
                new Weapon(Damage.NormalCutNormalGrowth(3, 1)),
                new Weapon(Damage.NormalCutNormalGrowth(1, 0)),
                new Weapon(Damage.DivisorNormalGrowth(2,0))
            };
            var expected = new[] {weapons[1], weapons[2], weapons[3], weapons[3], weapons[1]};
            var calculator = new KillCalculator(20);

            // act
            var result = calculator.CalculateKill(weapons, 15).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateKill_MultipleWeapons_OneHit_IsFastest()
        {
            // arrange
            var weapons = new[]
            {
                new Weapon(Damage.NormalCutNormalGrowth(1, 0)),
                new Weapon(Damage.NormalCutNormalGrowth(2, 0))
            };
            var expected = new[] {weapons[1]};
            var calculator = new KillCalculator(20);

            // act
            var result = calculator.CalculateKill(weapons, 2).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateKill_MultipleWeapons_MultipleHits_IsFastest()
        {
            // arrange
            var weapons = new[]
            {
                new Weapon(Damage.DivisorNormalGrowth(2, 0)),
                new Weapon(Damage.NormalCutNormalGrowth(8, 1)),
                new Weapon(Damage.NormalCutDoubleGrowth(3)),
                new Weapon(Damage.NormalCutNormalGrowth(5, 0)),
            };
            var expected = new[] {weapons[0], weapons[0], weapons[3]};
            var calculator = new KillCalculator(20);

            // act
            var result = calculator.CalculateKill(weapons, 20).ToArray();

            // assert
            CollectionAssert.AreEqual(expected, result);
        }
    }
}