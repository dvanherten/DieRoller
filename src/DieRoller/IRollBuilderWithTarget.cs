namespace DieRoller
{
    public interface IRollBuilderWithTarget : IRollBuilderWithReroll 
    {
        IRollBuilderWithReroll WithReroll(IRerollBehaviour rerollOptions);
    }
}