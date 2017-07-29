namespace DieRoller
{
    public interface IRollBuilderWithTarget : IBuildableRoll
    {
        IRollBuilderWithReroll WithReroll(IRerollBehaviour rerollOptions);
    }
}