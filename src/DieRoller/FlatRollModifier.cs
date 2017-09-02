using System;

namespace DieRoller
{
    public class FlatRollModifier : IRollModifier
    {
        public int ModifierValue { get; }

        public FlatRollModifier(int modifier)
        {
            ModifierValue = modifier;
        }

        public int GetModifiedTarget(int target)
        {
            return target + ModifierValue * -1;
        }

        public int ModifyRoll(int sideRolled)
        {
            return Math.Max(1, sideRolled + ModifierValue);
        }

        public override bool Equals(object obj)
        {
            var second = obj as FlatRollModifier;

            return ModifierValue == second?.ModifierValue;
        }

        protected bool Equals(FlatRollModifier other)
        {
            return ModifierValue == other.ModifierValue;
        }

        public override int GetHashCode()
        {
            return ModifierValue;
        }
    }
}