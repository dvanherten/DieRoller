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
        public void GivenD6_NoReroll_NoModifier_ExplicitBuilder(int valueAndAboveTarget)
        {
            var roll = RollBuilder.WithDie(Die.D6)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.None)
                .WithModifier(0)
                .Build();

            // Calc - SuccessfulRollCount / Sides
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
        public void GivenD6_RerollFailures_NoModifier_ExplicitBuilder(int valueAndAboveTarget)
        {
            var die = Die.D6;
            var roll = RollBuilder.WithDie(die)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .WithReroll(Reroll.Failures)
                .WithModifier(0)
                .Build();

            // Calc - (SuccessfulRollCount / Sides) + ((InverseOfSuccessfulRollCount / Sides) * (SuccessfulRollCount / Sides))
            var decimalSides = (decimal)die.Sides;
            var successfulRollCount = die.Sides + 1 - valueAndAboveTarget;
            var expectedProbability = successfulRollCount / decimalSides + (die.Sides - successfulRollCount) / decimalSides * (successfulRollCount / decimalSides);
            CheckProbability(roll, expectedProbability);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenD6_NoReroll_NoModifier_AsDefaultsInBuilder(int valueAndAboveTarget)
        {

            var roll = RollBuilder.WithDie(Die.D6)
                .Targeting(Target.ValueAndAbove(valueAndAboveTarget))
                .Build();

            // Calc - SuccessfulRollCount / Sides
            var expectedProbability = (7 - valueAndAboveTarget) / 6m;
            CheckProbability(roll, expectedProbability);
        }

        private void CheckProbability(Roll roll, decimal expectedProbability)
        {
            var actualProbability = roll.CalculateProbability();
            _output.WriteLine($"Probability: {actualProbability}");
            actualProbability.Should().Be(expectedProbability);
        }
    }
}