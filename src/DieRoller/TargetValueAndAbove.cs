using System.Collections.Generic;
using System.Linq;

namespace DieRoller
{
    public class TargetValueAndAbove : IRollTarget
    {
        internal TargetValueAndAbove(int value)
        {
            Target = value;
        }

        public int Target { get; }

        public IEnumerable<int> GetSuccessfulSides(int dieSides)
        {
            for (int i = 1; i <= dieSides; i++)
                if (i >= Target)
                    yield return i;
        }

        public IEnumerable<int> GetModifiedSuccessfulSides(int dieSides, IRollModifier modifier)
        {
            var modifiedTarget = modifier.GetModifiedTarget(Target);
            for (int i = 1; i <= dieSides; i++)
                if (i >= modifiedTarget)
                    yield return i;
        }

        public override string ToString()
        {
            return $"{Target}+";
        }
    }
}