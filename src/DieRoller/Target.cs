namespace DieRoller
{
    public class Target
    {
        public static IRollTarget ValueAndAbove(int value)
        {
            return new TargetValueAndAbove(value);
        }
    }
}