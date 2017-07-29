using System.Linq;

namespace DieRoller
{
    public class RerollOnes : IRerollBehaviour
    {
        public decimal CalculateProbability(Die die, IRollTarget target)
        {
            int[] successfulSides = target.GetSuccessfulSides(die.Sides).ToArray();
            if (successfulSides.Contains(1))
                return 0; // Can't have a re-roll if 1 is a success.

            return die.CalculateProbability(1) * die.CalculateProbability(target.GetSuccessCount(die.Sides));
        }
    }
}