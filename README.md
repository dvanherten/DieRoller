# DieRoller
A simple library for programmatically calculating probabilities and simulating scenarios 
of the rolling of a single die.

This library is a generic tool that generally follows the rules set out by 40k, but with no 
actual tie to the rule set of 40k.

## Creating a Roll

For the purposes of this tool, a Roll is the actions performed to get a single result. Meaning 
that a roll may actually be two rolls if you re-rolled, but these two rolls are made with the 
desire for a single result.

All rolls are created using the RollBuilder class. Using the fluent syntax, configure the Die 
to use, the target, the reroll behaviour and a modifier to the roll.

This simulates and is written in the order you would do this if you were actually rolling the die.
I want to: 
* Roll a 6 Sided Die
* Targetting a 3+ on the die
* I will reroll ones.
* Add one ot the final roll in an effort to get to 3+

```csharp
// The above scenario using the builder.
var roll = RollBuilder.WithDie(Die.D6)
    .Targeting(Target.ValueAndAbove(3))
    .WithReroll(Reroll.Ones)
    .WithModifier(1)
    .Build();
```

## Using a Roll

Now that you have a roll you can call two methods. You can calculate the probability that the roll 
will be a success or generate a scenario where a random roll will be provided to you.

It is important to note that Rerolling in this library takes place *BEFORE* the modifier.

### Probability

To calculate probability, call the CalculateProbability method on the roll. This will return a decimal between 0 and 1 
with the probability of the roll you have built.

```csharp
var result = roll.CalculateProbability();
```
### Simulation

To run a simulation, call the Simulate method on the roll. This will use a random number generator and play through the 
roll that you have built. This will return a Roll Result which has properties that explain what happened during the roll's 
exectution. 
