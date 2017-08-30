namespace DieRoller
{
    public class NoModifier : IRollModifier
    {
        public int GetModifiedTarget(int target)
        {
            return target;
        }
    }
}