namespace DieRoller
{
    /// <summary>
    /// Fluent builder to aid in the creation of a valid <see cref="Roll"/>.
    /// </summary>
    public class RollBuilder : IRollBuilderWithDie, IRollBuilderWithTarget, IRollBuilderWithReroll
    {
        public Die Die { get; }
        public IRollTarget Target { get; private set; }
        public IRerollBehaviour RerollBehaviour { get; private set; } = Reroll.None;
        public IRollModifier RollModifier { get; private set; } = new NoModifier();
        

        private RollBuilder(Die die)
        {
            Die = die;
        }

        public IRollBuilderWithTarget Targeting(IRollTarget target)
        {
            Target = target;
            return this;
        }

        public IBuildableRoll WithModifier(int modifier)
        {
            RollModifier = new FlatRollModifier(modifier);
            return this;
        }

        public IRollBuilderWithReroll WithReroll(IRerollBehaviour rerollOptions)
        {
            RerollBehaviour = rerollOptions;
            return this;
        }

        public Roll Build()
        {
            return new Roll(Die, Target, RerollBehaviour, RollModifier);
        }

        public static IRollBuilderWithDie WithDie(Die die)
        {
            return new RollBuilder(die);
        }
    }
}