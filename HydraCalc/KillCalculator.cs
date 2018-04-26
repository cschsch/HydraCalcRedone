using System;
using System.Collections.Generic;
using System.Linq;
using HydraCalc.Weapon;
// ReSharper disable PossibleMultipleEnumeration

namespace HydraCalc
{
    public class KillCalculator : ICalculateKill
    {
        public int MaxAmountOfSteps
        {
            get => _maxAmountOfSteps;
            set
            {
                if (value <= 0 || value >= 100) throw new ArgumentOutOfRangeException(nameof(value), "Must be greater than 0 and less than 100");
                _maxAmountOfSteps = value;
            }
        }

        private int _maxAmountOfSteps;

        public KillCalculator(int maxAmountOfSteps) => MaxAmountOfSteps = maxAmountOfSteps;

        public IEnumerable<IWeapon> CalculateKill(IEnumerable<IWeapon> weapons, int amountOfHeads)
        {
            if(weapons is null) throw new ArgumentNullException(nameof(weapons), "Cannot be null");
            if(!weapons.Any()) throw new ArgumentException("Cannot be empty", nameof(weapons));
            if(amountOfHeads <= 0) throw new ArgumentOutOfRangeException(nameof(amountOfHeads), "Must be greater than 0");

            var root = new Node<Step>(null, new Step(null, amountOfHeads, 0));
            return CalculateKillRecursive(weapons, root, new Queue<Node<Step>>())?.GetParents().Select(path => path.Weapon) ?? Enumerable.Empty<IWeapon>();
        }

        private Node<Step> CalculateKillRecursive(IEnumerable<IWeapon> weaponSet, Node<Step> currentStep, Queue<Node<Step>> stepsToProcess)
        {
            var amountOfHeads = currentStep.Value.AmountOfHeads;
            var numberOfStep = currentStep.Value.NumberOfStep;

            if(numberOfStep >= MaxAmountOfSteps) return null;

            foreach (var weapon in weaponSet)
            {
                var cutAmount = weapon.GetHitDifference(amountOfHeads);
                if(cutAmount != 0) currentStep.AddChild(currentStep, new Step(weapon, amountOfHeads - cutAmount, numberOfStep + 1));
            }

            foreach (var childStep in currentStep)
            {
                if (childStep.Value.AmountOfHeads == 0) return childStep;
                stepsToProcess.Enqueue(childStep);
            }

            foreach (var _ in stepsToProcess)
            {
                return CalculateKillRecursive(weaponSet, stepsToProcess.Dequeue(), stepsToProcess);
            }

            return null;
        }
    }
}