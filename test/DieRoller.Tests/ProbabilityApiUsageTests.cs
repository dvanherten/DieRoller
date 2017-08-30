using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace DieRoller.Tests
{
    /// <summary>
    ///     This test class will use the public fluent <see cref="RollBuilder" /> api and validate expected calculations.
    /// </summary>
    public class ProbabilityApiUsageTests
    {
        private readonly ITestOutputHelper _output;

        public ProbabilityApiUsageTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_NoReroll_NoModifier(int valueAndAboveTarget)
        {
            var roll = RollBuilder.WithDie(Die.D6)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .Build();

            // Calc - SuccessfulSideCount / Sides
            var expectedProbability = (7 - valueAndAboveTarget) / 6m;
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_RerollFailures(int valueAndAboveTarget)
        {
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.Failures)
                .Build();

            // Calc - (SuccessfulSideCount / Sides) + ((InverseOfSuccessfulSideCount / Sides) * (SuccessfulSideCount / Sides))
            var decimalSides = (decimal)die.TotalSides;
            var successfulSideCount = die.TotalSides + 1 - valueAndAboveTarget;
            var expectedProbability = successfulSideCount / decimalSides + (die.TotalSides - successfulSideCount) / decimalSides * (successfulSideCount / decimalSides);
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_RerollOnes(int valueAndAboveTarget)
        {
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.Ones)
                .Build();

            // Calc - (SuccessfulSideCount / Sides) + ((1 / Sides) * (SuccessfulSideCount / Sides))
            var decimalSides = (decimal)die.TotalSides;
            var successfulSideCount = die.TotalSides + 1 - valueAndAboveTarget;
            var expectedProbability = successfulSideCount / decimalSides + 1 / decimalSides * (successfulSideCount / decimalSides);
            CheckProbability(roll, expectedProbability);
        }

        [Fact]
        public void GivenD6_RerollOnes_WhenOneIsSuccess()
        {
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(1))
                .WithReroll(Reroll.Ones)
                .Build();

            // Calc - If the base probability is already 100%, reroll should not increase it further as you should not be rerolling.
            var expectedProbability = 1m;
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_NoReroll_ModifierIncreaseByOne(int valueAndAboveTarget)
        {
            var modifier = 1;
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.None)
                .WithModifier(modifier)
                .Build();

            // Calc - (SuccessfulSideCount + ModifierAmount) / Sides
            var decimalSides = (decimal)die.TotalSides;
            var successfulSideCount = die.TotalSides + 1 - valueAndAboveTarget;
            var expectedProbability = (successfulSideCount + modifier) / decimalSides;
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_RerollOnes_ModifierIncreaseByOne(int valueAndAboveTarget)
        {
            var modifier = 1;
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.Ones)
                .WithModifier(modifier)
                .Build();

            // Calc - See CalcRerollWithModifier
            var successfulSideCount = die.TotalSides + 1 - valueAndAboveTarget;
            var expectedProbability = CalcProbabilityForRerollWithModifier(die.TotalSides, successfulSideCount, 1, modifier);
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_RerollFailures_ModifierIncreaseByOne(int valueAndAboveTarget)
        {
            var modifier = 1;
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.Failures)
                .WithModifier(modifier)
                .Build();

            // Calc - See CalcRerollWithModifier
            var successfulSideCount = die.TotalSides + 1 - valueAndAboveTarget;
            var rerollSides = die.TotalSides - successfulSideCount;
            var expectedProbability = CalcProbabilityForRerollWithModifier(die.TotalSides, successfulSideCount, rerollSides, modifier);
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_NoReroll_ModifierDecreaseByOne(int valueAndAboveTarget)
        {
            var modifier = -1;
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.None)
                .WithModifier(-1)
                .Build();

            // Calc - ((SuccessfulSideCount + ModifierAmount) / Sides)
            var decimalSides = (decimal)die.TotalSides;
            var successfulSideCount = die.TotalSides + 1 - valueAndAboveTarget;
            var expectedProbability = (successfulSideCount + modifier) / decimalSides;
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_RerollOnes_ModifierDecreaseByOne(int valueAndAboveTarget)
        {
            var modifier = -1;
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.Ones)
                .WithModifier(modifier)
                .Build();

            // Calc - See CalcRerollWithModifier
            var successfulSideCount = die.TotalSides + 1 - valueAndAboveTarget;
            var expectedProbability = CalcProbabilityForRerollWithModifier(die.TotalSides, successfulSideCount, 1, modifier);
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_RerollFailures_ModifierDecreaseByOne(int valueAndAboveTarget)
        {
            var modifier = -1;
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.Failures)
                .WithModifier(modifier)
                .Build();

            // Calc - See CalcRerollWithModifier
            var successfulSideCount = die.TotalSides + 1 - valueAndAboveTarget;
            var rerollSides = die.TotalSides - successfulSideCount;
            var expectedProbability = CalcProbabilityForRerollWithModifier(die.TotalSides, successfulSideCount, rerollSides, modifier);
            CheckProbability(roll, expectedProbability);
        }

        /// <summary>Calculate the expected probability when a modifier and a reroll are in play.</summary>
        /// <remarks>
        /// In a world without rerolls, the roll is able to be directly impacted by the modifier. If there were 3 successful sides (4+) 
        /// before and you have a +1 modifier, then there are now 4 successful sides (3+). Adding rerolls to the mix complicates things as
        /// rerolls take place BEFORE a modifier. This means that the initial roll will only sometimes be affected by the modifier for the 
        /// purposes of calculating probability.
        /// 
        /// If the modifier is positive and does not overlap with the reroll sides include the modifier in the successful sides.
        /// If it overlaps with the reroll sides, only include the part of the modifier that is not captured by rerolls.
        /// Example: Rerolling 1's wont affect a modifier of +1 on a 3+ target during the initial roll, but rerolling all failures would need to not include the modifier.
        /// 
        /// If the modifier is negative always include it in the initial roll successful sides
        /// 
        /// Successful sides for the purposes of calculating how often a reroll takes place never consider the modifier.
        /// 
        /// The modifier will always affect the successful sides of the reroll's roll.
        /// </remarks>
        private static decimal CalcProbabilityForRerollWithModifier(int dieSides, int successfulSides, int rerollSides, int modifier)
        {
            if (rerollSides + successfulSides > dieSides)
                throw new Exception("Test invalid, you can't reroll successful rolls.");

            var decimalSides = (decimal)dieSides;

            var initialModifierAmount = GetInitialRollModifierAmount(dieSides, successfulSides, rerollSides, modifier);

            return (successfulSides + initialModifierAmount) / decimalSides + rerollSides / decimalSides * ((successfulSides + modifier) / decimalSides);
        }

        /// <summary>
        /// Get the intial roll modifier as a re-roll may affect the successful roll amount.
        /// </summary>
        private static int GetInitialRollModifierAmount(int dieSides, int successfulSides, int rerollSides, int modifier)
        {
            if (modifier > 0)
            {
                var overlap = dieSides - (successfulSides + rerollSides + modifier);
                if (overlap < 0)
                    return modifier + overlap;
            }
            return modifier;
        }

        private void CheckProbability(Roll roll, decimal expectedProbability)
        {
            var actualProbability = roll.CalculateProbability();
            expectedProbability = Math.Min(1m, Math.Max(0m, expectedProbability));
            _output.WriteLine($"Expected Probability: {expectedProbability:P}");
            _output.WriteLine($"Actual Probability: {actualProbability:P}");
            actualProbability.Should().Be(expectedProbability);
        }
    }
}