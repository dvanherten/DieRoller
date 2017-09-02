namespace DieRoller
{
    public interface IRollModifier
    {
        int ModifierValue { get; }
        int GetModifiedTarget(int target);
        int ModifyRoll(int sideRolled);
    }
}