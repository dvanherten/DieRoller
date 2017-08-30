namespace DieRoller
{
    public interface IRollBuilderWithDie
    {
        IRollBuilderWithTarget Targeting(int target);
        IRollBuilderWithDie WithNumbers(INumberGenerator numberGenerator);
    }
}