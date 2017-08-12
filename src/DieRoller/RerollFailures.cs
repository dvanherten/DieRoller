using System.Linq;

namespace DieRoller
{
    public class RerollFailures : IRerollBehaviour
    {
        internal RerollFailures() { }

        public decimal CalculateProbability(Die die, IRollTarget target)
        {
            var failures = die.Sides - target.GetSuccessCount(die.Sides);
            return die.CalculateProbability(failures) * die.CalculateProbability(target.GetSuccessCount(die.Sides));
        }

        public bool RequiresReroll(SingleRollResult initial, IRollTarget target)
        {
            return !target.GetSuccessfulSides(initial.Die.Sides).Contains(initial.SideRolled);
        }
    }
}