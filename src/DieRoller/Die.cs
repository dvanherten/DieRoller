using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace DieRoller
{
    /// <summary>
    /// Represents a <see cref="Die"/>. A die must have at least two sides.
    /// </summary>
    public class Die
    {
        public Die(int sides)
        {
            if (sides <= 1) throw new ArgumentOutOfRangeException(nameof(sides), $"A {nameof(Die)} must have at least two sides. Was provided {sides}.");
            TotalSides = sides;
        }

        public int TotalSides { get; }

        public IEnumerable<int> GetSides()
        {
            for (int i = 1; i <= TotalSides; i++)
                yield return i;
        }

        /// <summary>
        /// Calculate success probability based on the number of successful sides provided.
        /// </summary>
        /// <param name="successfulSideCount">The amount of sides that indicate a successful roll.</param>
        /// <returns>Probability that the roll was successful.</returns>
        internal decimal CalculateProbability(int successfulSideCount)
        {
            Guard.Against.OutOfRange(successfulSideCount, nameof(successfulSideCount), 0, TotalSides);

            return (decimal)successfulSideCount / TotalSides;
        }
        
        public SingleRollResult Simulate(INumberGenerator numberGenerator)
        {
            var number = numberGenerator.GetNumber();
            number = Math.Abs(number);

            var remainder = number % TotalSides;
            return new SingleRollResult(this, remainder == 0 ? TotalSides : remainder);
        }

        public static Die D6 => new Die(6);
        public static Die D3 => new Die(3);
    }
}