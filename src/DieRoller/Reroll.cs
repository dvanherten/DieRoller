namespace DieRoller
{
    public static class Reroll
    {
        public static IRerollBehaviour Failures => new RerollFailures();
        public static IRerollBehaviour Ones => new RerollOnes();
        public static IRerollBehaviour None => new RerollNone();
    }

    public class RerollFailures : IRerollBehaviour
    {
        internal RerollFailures() { }

        public decimal CalculateProbability(Die die, IRollTarget target)
        {
            var failures = die.Sides - target.GetSuccessCount(die.Sides);
            return die.CalculateProbability(failures) * die.CalculateProbability(target.GetSuccessCount(die.Sides));
        }
    }

    public class RerollOnes : IRerollBehaviour
    {
        public decimal CalculateProbability(Die die, IRollTarget target)
        {
            throw new System.NotImplementedException();
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