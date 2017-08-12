namespace DieRoller
{
    public interface IRerollBehaviour
    {
        decimal CalculateProbability(Die die, IRollTarget target);
        bool RequiresReroll(SingleRollResult initial, IRollTarget target);
    }
}