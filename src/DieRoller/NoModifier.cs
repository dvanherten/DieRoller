namespace DieRoller
{
    public class NoModifier : IRollModifier
    {
        public int ModifierValue => 0;

        public int GetModifiedTarget(int target)
        {
            return target;
        }

        public int ModifyRoll(int sideRolled)
        {
            return sideRolled;
        }
    }
}