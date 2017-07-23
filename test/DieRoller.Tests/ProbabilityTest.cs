using System;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace DieRoller.Tests
{
    public class ProbabilityTest
    {
        private readonly ITestOutputHelper _output;

        public ProbabilityTest(ITestOutputHelper output)
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
        public void GivenSimpleD6Die_ProbabilityShouldCalculateCorrectly(int requiredRollOrHigher)
        {
            var expectedProb = (7 - requiredRollOrHigher) / 6m;
            var roll = new D6Die(requiredRollOrHigher);
            _output.WriteLine($"Probability: {roll.Probability}");
            roll.Probability.Should().Be(expectedProb);
        }
    }
}
