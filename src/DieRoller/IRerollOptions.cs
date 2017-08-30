using System.Collections.Generic;

namespace DieRoller
{
    public interface IRerollBehaviour
    {
        decimal CalculateProbability(Die die, IRollTarget target);
        IEnumerable<int> GetRerollSides(Die die, IRollTarget target);
        bool RequiresReroll(SingleRollResult initial, IRollTarget target);
    }
}