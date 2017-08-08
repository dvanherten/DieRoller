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
                .Targeting(Target.ValueAndAbove(numberToGenerate))
                .Build();

            var result = roll.Simulate();

            result.Should().Be(expected);
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
                .Targeting(Target.ValueAndAbove(numberToGenerate))
                .Build();

            var result = roll.Simulate();

            result.Should().Be(expected);
        }
    }

    public class ForcedNumberGenerator : INumberGenerator
    {
        public int NumberToReturn { get; }

        public ForcedNumberGenerator(int numberToReturn)
        {
            NumberToReturn = numberToReturn;
        }

        public int GetNumber()
        {
            return NumberToReturn;
        }
    }
}
