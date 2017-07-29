using System;

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
            Sides = sides;
        }

        public int Sides { get; }

        /// <summary>
        /// Calculate success probability based on the number of successful sides provided.
        /// </summary>
        /// <param name="successfulSideCount">The amount of sides that indicate a successful roll.</param>
        /// <returns>Probability that the roll was successful.</returns>
        internal decimal CalculateProbability(int successfulSideCount)
        {
            if (successfulSideCount < 0) throw new ArgumentOutOfRangeException(nameof(successfulSideCount));
            if (successfulSideCount > Sides) throw new ArgumentOutOfRangeException(nameof(successfulSideCount));

            return (decimal)successfulSideCount / Sides;
        }

        public static Die D6 => new Die(6);
        public static Die D3 => new Die(3);
    }
}