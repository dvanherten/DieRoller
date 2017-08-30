namespace DieRoller
{
    public class FlatRollModifier : IRollModifier
    {
        public int Modifier { get; }

        public FlatRollModifier(int modifier)
        {
            Modifier = modifier;
        }

        public int GetModifiedTarget(int target)
        {
            return target + Modifier * -1;
        }

        public override bool Equals(object obj)
        {
            var second = obj as FlatRollModifier;

            return Modifier == second?.Modifier;
        }

        protected bool Equals(FlatRollModifier other)
        {
            return Modifier == other.Modifier;
        }

        public override int GetHashCode()
        {
            return Modifier;
        }
    }
}