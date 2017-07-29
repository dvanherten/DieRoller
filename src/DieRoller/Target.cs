namespace DieRoller
{
    public class Target : IRollTarget
    {
        private Target(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public int GetSuccessCount(int dieSides)
        {
            return dieSides + 1 - Value;
        }

        public static Target ValueAndAbove(int value)
        {
            return new Target(value);
        }
    }
}