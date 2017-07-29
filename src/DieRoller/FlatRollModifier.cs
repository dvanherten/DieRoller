namespace DieRoller
{
    public class FlatRollModifier : IRollModifier
    {
        private readonly int _modifier;

        public FlatRollModifier(int modifier)
        {
            _modifier = modifier;
        }
    }
}