using System.Collections.Generic;

namespace DieRoller
{
    public class RerollNone : IRerollBehaviour
    {
        internal RerollNone()
        {
            
        }

        public decimal CalculateProbability(Die die, IRollTarget target)
        {
            return 0;
        }

        public IEnumerable<int> GetRerollSides(Die die, IRollTarget target)
        {
            return new int[0];
        }

        public bool RequiresReroll(SingleRollResult initial, IRollTarget target)
        {
            return false;
        }
    }
}