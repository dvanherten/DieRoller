using System.Collections.Generic;

namespace DieRoller
{
    public interface IRollTarget
    {
        IEnumerable<int> GetSuccessfulSides(int dieSides);
        IEnumerable<int> GetModifiedSuccessfulSides(int dieSides);
    }
}