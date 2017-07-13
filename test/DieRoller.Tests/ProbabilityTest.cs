using FluentAssertions;
using Xunit;

namespace DieRoller.Tests
{
    public class ProbabilityTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void GivenSimpleD6Die_ProbabilityShouldCalculateCorrectly(int requiredRollOrHigher)
        {
            var roll = new D6Die(requiredRollOrHigher);
            roll.Probability.Should().Be(requiredRollOrHigher / 6m);
        }
    }
}
