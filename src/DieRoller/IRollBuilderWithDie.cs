namespace DieRoller
{
    public interface IRollBuilderWithDie
    {
        IRollBuilderWithTarget Targeting(IRollTarget target);
    }
}