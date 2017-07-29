namespace DieRoller
{
    public static class Reroll
    {
        public static IRerollBehaviour Failures => new RerollFailures();
        public static IRerollBehaviour Ones => new RerollOnes();
        public static IRerollBehaviour None => new RerollNone();
    }
}