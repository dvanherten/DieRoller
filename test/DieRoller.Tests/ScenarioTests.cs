using System;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace DieRoller.Tests
{
    public class ScenarioTests
    {
        private readonly ITestOutputHelper _output;

        public ScenarioTests(ITestOutputHelper output)
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
            var die = new D6Die();
            var expectedProb = (7 - requiredRollOrHigher) / 6m;
            var roll = new Roll(die, requiredRollOrHigher);
            _output.WriteLine($"Probability: {roll.Probability}");
            roll.Probability.Should().Be(expectedProb);
        }

        [Fact]
        public void GivenSimpleD6Die_WithExpectationLessThanOne_ShouldThrow()
        {
            var die = new D6Die();
            Action action = () =>
            {
                var roll = new Roll(die, 0);
            };
            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void GivenSimpleD6Die_WithNoDie_ShouldThrow()
        {
            Action action = () =>
            {
                var roll = new Roll(null, 1);
            };
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GivenMockedDieWithNoSides_ProbabilityShouldReturnZero()
        {
            var dieMock = new Mock<IDie>();
            dieMock.Setup(x => x.Sides).Returns(0);

            var roll = new Roll(dieMock.Object, 3);
            roll.Probability.Should().Be(0);
        }

        [Fact]
        public void GivenMockedDieWithNegativeSides_ProbabilityShouldReturnZero()
        {
            var dieMock = new Mock<IDie>();
            dieMock.Setup(x => x.Sides).Returns(-1);

            var roll = new Roll(dieMock.Object, 3);
            roll.Probability.Should().Be(0);
        }
    }
}
