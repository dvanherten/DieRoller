using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace DieRoller.Tests
{
    /// <summary>
    ///     This test class will use the public fluent <see cref="RollBuilder" /> api and validate expected scenario results.
    ///     Leverages a mocked random generator to force expected outcomes.
    /// </summary>
    public class ScenarioApiUsageTests
    {
        private readonly ITestOutputHelper _output;

        public ScenarioApiUsageTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(6, 6)]
        [InlineData(7, 1)]
        [InlineData(12, 6)]
        [InlineData(13, 1)]
        [InlineData(0, 6)]
        [InlineData(-1, 1)]
        [InlineData(-5, 5)]
        [InlineData(321321, 3)]
        public void GivenD6_NoReroll_NoModifier(int numberToGenerate, int expected)
        {
            var roll = RollBuilder.WithDie(Die.D6)
                .WithNumbers(new ForcedNumberGenerator(numberToGenerate))
                .Targeting(Target.ValueAndAbove(3))
                .Build();

            var result = roll.Simulate();

            _output.WriteLine(result.ToString());
            result.Final.Should().Be(expected);
        }

        [Theory]
        [InlineData(3, 1, 5, true, 5, true)]
        [InlineData(6, 1, 5, true, 5, false)]
        [InlineData(3, 5, 4, false, 5, true)]
        [InlineData(3, 2, 5, false, 2, false)]
        public void GivenD6_RerollOnes_NoModifier(int targeting, int initialRoll, int reroll, bool expectedReroll, int expectedFinal, bool expectedSuccess)
        {
            var roll = RollBuilder.WithDie(Die.D6)
                .WithNumbers(new ForcedNumberGenerator(initialRoll, reroll))
                .Targeting(Target.ValueAndAbove(targeting))
                .WithReroll(Reroll.Ones)
                .Build();

            var result = roll.Simulate();

            _output.WriteLine(result.ToString());
            ValidateRollResult(result, initialRoll, reroll, expectedReroll, expectedFinal, expectedSuccess);
        }

        [Theory]
        [InlineData(3, 1, 5, true, 5, true)]
        [InlineData(6, 1, 5, true, 5, false)]
        [InlineData(3, 5, 4, false, 5, true)]
        [InlineData(3, 2, 5, true, 5, true)]
        public void GivenD6_RerollFailures_NoModifier(int targeting, int initialRoll, int reroll, bool expectedReroll, int expectedFinal, bool expectedSuccess)
        {
            var roll = RollBuilder.WithDie(Die.D6)
                .WithNumbers(new ForcedNumberGenerator(initialRoll, reroll))
                .Targeting(Target.ValueAndAbove(targeting))
                .WithReroll(Reroll.Failures)
                .Build();

            var result = roll.Simulate();

            _output.WriteLine(result.ToString());
            ValidateRollResult(result, initialRoll, reroll, expectedReroll, expectedFinal, expectedSuccess);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 1)]
        [InlineData(5, 2)]
        [InlineData(6, 3)]
        [InlineData(7, 1)]
        [InlineData(12, 3)]
        [InlineData(14, 2)]
        [InlineData(0, 3)]
        [InlineData(-1, 1)]
        [InlineData(-5, 2)]
        [InlineData(321321, 3)]
        public void GivenD3_NoReroll_NoModifier(int numberToGenerate, int expected)
        {
            var roll = RollBuilder.WithDie(Die.D3)
                .WithNumbers(new ForcedNumberGenerator(numberToGenerate))
                .Targeting(Target.ValueAndAbove(3))
                .Build();

            var result = roll.Simulate();

            _output.WriteLine(result.ToString());
            result.Final.Should().Be(expected);
        }

        [Theory]
        [InlineData(3, 1, 3, true, 3, true)]
        [InlineData(2, 1, 1, true, 1, false)]
        [InlineData(3, 2, 3, false, 2, false)]
        [InlineData(2, 1, 1, true, 1, false)]
        public void GivenD3_RerollOnes_NoModifier(int targeting, int initialRoll, int reroll, bool expectedReroll, int expectedFinal, bool expectedSuccess)
        {
            var roll = RollBuilder.WithDie(Die.D3)
                .WithNumbers(new ForcedNumberGenerator(initialRoll, reroll))
                .Targeting(Target.ValueAndAbove(targeting))
                .WithReroll(Reroll.Ones)
                .Build();

            var result = roll.Simulate();

            _output.WriteLine(result.ToString());
            ValidateRollResult(result, initialRoll, reroll, expectedReroll, expectedFinal, expectedSuccess);
        }

        [Theory]
        [InlineData(3, 1, 3, true, 3, true)]
        [InlineData(2, 1, 1, true, 1, false)]
        [InlineData(3, 2, 3, true, 3, true)]
        [InlineData(3, 2, 1, true, 1, false)]
        [InlineData(2, 1, 1, true, 1, false)]
        [InlineData(3, 3, 2, false, 3, true)]
        public void GivenD3_RerollFailures_NoModifier(int targeting, int initialRoll, int reroll, bool expectedReroll, int expectedFinal, bool expectedSuccess)
        {
            var roll = RollBuilder.WithDie(Die.D3)
                .WithNumbers(new ForcedNumberGenerator(initialRoll, reroll))
                .Targeting(Target.ValueAndAbove(targeting))
                .WithReroll(Reroll.Failures)
                .Build();

            var result = roll.Simulate();

            _output.WriteLine(result.ToString());
            ValidateRollResult(result, initialRoll, reroll, expectedReroll, expectedFinal, expectedSuccess);
        }

        private static void ValidateRollResult(RollResult result, int initialRoll, int reroll, bool expectedReroll, int expectedFinal, bool expectedSuccess)
        {
            result.InitialRollResult.SideRolled.Should().Be(initialRoll);
            if (expectedReroll)
                result.RerollResult.SideRolled.Should().Be(reroll);
            else
                result.RerollResult.Should().BeNull();
            result.Final.Should().Be(expectedFinal);
            result.IsSuccessful.Should().Be(expectedSuccess);
        }
    }
}
