using System.Collections.Generic;
using System.Linq;

namespace DieRoller
{
    public class TargetValueAndAbove : IRollTarget
    {
        private readonly IRollModifier _modifier;

        internal TargetValueAndAbove(int value, IRollModifier modifier)
        {
            _modifier = modifier;
            Target = value;
        }

        public int Target { get; }
        public int ModifiedTarget => _modifier.GetModifiedTarget(Target);

        public IEnumerable<int> GetSuccessfulSides(int dieSides)
        {
            for (int i = 1; i <= dieSides; i++)
                if (i >= Target)
                    yield return i;
        }

        public IEnumerable<int> GetModifiedSuccessfulSides(int dieSides)
        {
            for (int i = 1; i <= dieSides; i++)
                if (i >= ModifiedTarget)
                    yield return i;
        }

        public override string ToString()
        {
            return $"{Target}+";
        }
    }
}