namespace DieRoller
{
    public interface IRollBuilderWithReroll : IBuildableRoll
    {
        IBuildableRoll WithModifier(int modifier);
    }
}