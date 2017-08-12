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

        public bool RequiresReroll(SingleRollResult initial, IRollTarget target)
        {
            return false;
        }
    }
}