namespace DieRoller
{
    public class SingleRollResult
    {
        public SingleRollResult(Die die, int sideRolled)
        {
            Die = die;
            SideRolled = sideRolled;
        }

        public Die Die { get; }
        public int SideRolled { get; }
    }
}