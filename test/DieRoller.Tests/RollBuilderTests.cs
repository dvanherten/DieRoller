using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace DieRoller.Tests
{
    public class RollBuilderTests
    {
        [Fact]
        public void DefaultRollModifier_ShouldBeNoModifier()
        {
            var rollBuilder = RollBuilder.WithDie(Die.D6)
                .Targeting(Target.ValueAndAbove(4)) as RollBuilder;
            
            rollBuilder.RollModifier.GetType().Should().Be<NoModifier>();
        }

        [Fact]
        public void DefaultReroll_ShouldBeNone()
        {
            var rollBuilder = RollBuilder.WithDie(Die.D6)
                .Targeting(Target.ValueAndAbove(4)) as RollBuilder;
            
            rollBuilder.RerollBehaviour.GetType().Should().Be<RerollNone>();
        }
    }
}