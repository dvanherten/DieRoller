using System.Collections.Generic;
using System.Linq;

namespace DieRoller
{
    public class RerollOnes : IRerollBehaviour
    {
        public decimal CalculateProbability(Die die, IRollTarget target, IRollModifier modifier)
        {
            return die.CalculateProbability(1) * die.CalculateProbability(target.GetModifiedSuccessfulSides(die.TotalSides, modifier).Count());
        }

        public IEnumerable<int> GetRerollSides(Die die, IRollTarget target)
        {
            return new[] {1};
        }

        public bool RequiresReroll(SingleRollResult initial, IRollTarget target)
        {
            return initial.SideRolled == 1;
        }
    }
}