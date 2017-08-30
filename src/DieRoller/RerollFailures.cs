using System.Collections.Generic;
using System.Linq;

namespace DieRoller
{
    public class RerollFailures : IRerollBehaviour
    {
        internal RerollFailures() { }

        public decimal CalculateProbability(Die die, IRollTarget target)
        {
            var failures = die.TotalSides - target.GetSuccessfulSides(die.TotalSides).Count();
            return die.CalculateProbability(failures) * die.CalculateProbability(target.GetModifiedSuccessfulSides(die.TotalSides).Count());
        }

        public IEnumerable<int> GetRerollSides(Die die, IRollTarget target)
        {
            return die.GetSides().Except(target.GetSuccessfulSides(die.TotalSides));
        }

        public bool RequiresReroll(SingleRollResult initial, IRollTarget target)
        {
            return !target.GetSuccessfulSides(initial.Die.TotalSides).Contains(initial.SideRolled);
        }
    }
}