namespace DieRoller
{
    public interface IRollBuilderWithDie
    {
        IRollBuilderWithTarget Targeting(IRollTarget target);
        IRollBuilderWithDie WithNumbers(INumberGenerator numberGenerator);
    }
}