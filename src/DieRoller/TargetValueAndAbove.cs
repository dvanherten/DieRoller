using System.Collections.Generic;
using System.Linq;

namespace DieRoller
{
    public class TargetValueAndAbove : IRollTarget
    {
        internal TargetValueAndAbove(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public int GetSuccessCount(int dieSides)
        {
            return GetSuccessfulSides(dieSides).Count();
        }

        public IEnumerable<int> GetSuccessfulSides(int dieSides)
        {
            for (int i = 1; i <= dieSides; i++)
                if (i >= Value)
                    yield return i;
        }
    }
}