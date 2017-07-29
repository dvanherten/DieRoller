namespace DieRoller
{
    public interface IRollBuilderWithTarget : IBuildableRoll
    {
        IRollBuilderWithReroll WithReroll(RerollOptions rerollOptions);
    }
}