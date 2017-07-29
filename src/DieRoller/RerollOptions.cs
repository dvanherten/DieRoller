namespace DieRoller
{
    public static class Reroll
    {
        public static IRerollBehaviour Failures => new RerollFailures();
        public static IRerollBehaviour None => new RerollNone();
    }

    public class RerollFailures : IRerollBehaviour
    {
        public decimal CalculateProbability(Die die, IRollTarget target)
        {
            var failures = die.Sides - target.GetSuccessCount(die.Sides);
            return die.CalculateProbability(failures) * die.CalculateProbability(target.GetSuccessCount(die.Sides));
        }
    }

    public class RerollNone : IRerollBehaviour
    {
        internal RerollNone()
        {
            
        }

        public decimal CalculateProbability(Die die, IRollTarget target)
        {
            return 0;
        }
    }
}