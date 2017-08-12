using System;

namespace DieRoller
{
    /// <summary>
    /// Implementation using random to be used at runtime.
    /// </summary>
    public class RandomNumberGenerator : INumberGenerator
    {
        public static Random Random = new Random();
        public int GetNumber()
        {
            return Random.Next();
        }
    }
}