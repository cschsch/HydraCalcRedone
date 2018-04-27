# HydraCalcRedone
Easy to use library of HydraCalc written in C#. Now supports a greater amount of steps and is much more efficient.

# Usage

- Create weapons:

  `var weapon = new Weapon(IDamage damageType)`

- Possible DamageTypes:

  `Damage.NormalCutNormalGrowth(int cut, int growth)` 
  
  - Weapon cuts "cut" heads; "growth" heads grow back
  
  `Damage.NormalCutDoubleGrowth(int cut)` 
  
  - Weapon cuts "cut" heads; double the heads grow back
  
  `Damage.DivisorNormalGrowth(int cutFactor, int growth)` 
  
  - Weapon divides heads by "cutFactor"; "growth" heads grow back
  
  `Damage.DivisorDoubleGrowth(int cutFactor)`
  
  - Weapon divides heads by "cutFactor"; double the heads grow back
  
- Create KillCalculator:

  `var calculator = new KillCalculator(int maxAmountOfSteps)`
  
  - "maxAmountOfSteps" is the maximum depth the calculation will go
  
- Call CalculateKill:

  `calculator.CalculateKill(IEnumerable<IWeapon> weapons, int amountOfHeads)`
  
  - "weapons" is the sequence of weapons available
  
  - "amountOfHeads" is the amount of heads the hydra has
  
  - Result is empty if no kill has been found or the sequence of weapons to use if it has
