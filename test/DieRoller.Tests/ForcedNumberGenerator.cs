using System;
using FluentAssertions;
using Xunit;

namespace DieRoller.Tests
{
    /// <summary>
    /// This ForcedNumberGenerator will be used in tests. It does assume a certain order of rolling.
    /// A generator is used twice within a simulation sequence. Once for the initial roll, then again for the reroll. 
    /// We are using this knowledge within our tests.
    /// </summary>
    public class ForcedNumberGenerator : INumberGenerator
    {
        private int _counter;
        public int[] Numbers { get; }

        public ForcedNumberGenerator(params int[] numbers)
        {
            if (numbers.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(numbers));
            Numbers = numbers;
        }

        public int GetNumber()
        {
            var numberToReturn = Numbers[_counter];
            _counter++;
            if (_counter == Numbers.Length)
                _counter = 0;
            return numberToReturn;
        }
    }

    public class ForcedNumberGeneratorTests
    {
        [Fact]
        public void SingleNumber_ConsistentReturn()
        {
            var generator = new ForcedNumberGenerator(5);

            generator.GetNumber().Should().Be(5);
            generator.GetNumber().Should().Be(5);
            generator.GetNumber().Should().Be(5);
            generator.GetNumber().Should().Be(5);
        }

        [Fact]
        public void MultipleNumbers_ShouldAlternateThenLoop()
        {
            var generator = new ForcedNumberGenerator(5, 2, 6);

            generator.GetNumber().Should().Be(5);
            generator.GetNumber().Should().Be(2);
            generator.GetNumber().Should().Be(6);
            generator.GetNumber().Should().Be(5);
        }

        [Fact]
        public void MultipleNumbers_ShouldThrowWithNoNumbers()
        {
            var exception = Assert.Throws<ArgumentException>(() => new ForcedNumberGenerator());
            exception.ParamName.Should().Be("numbers");
        }
    }
}