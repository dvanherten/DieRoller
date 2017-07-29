using System.Collections.Generic;

namespace DieRoller
{
    public interface IRollTarget
    {
        int GetSuccessCount(int dieSides);
        IEnumerable<int> GetSuccessfulSides(int dieSides);
    }
}