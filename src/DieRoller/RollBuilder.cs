namespace DieRoller
{
    /// <summary>
    /// Fluent builder to aid in the creation of a valid <see cref="Roll"/>.
    /// </summary>
    public class RollBuilder : IRollBuilderWithDie, IRollBuilderWithTarget, IRollBuilderWithReroll
    {
        public Die Die { get; }
        public INumberGenerator NumberGenerator { get; private set; } = new RandomNumberGenerator();
        public int Target { get; private set; }
        public IRerollBehaviour RerollBehaviour { get; private set; } = Reroll.None;
        public IRollModifier RollModifier { get; private set; } = new NoModifier();
        

        private RollBuilder(Die die)
        {
            Die = die;
        }

        public IRollBuilderWithTarget Targeting(int target)
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
            return new Roll(Die, new TargetValueAndAbove(Target), RerollBehaviour, RollModifier, NumberGenerator);
        }

        public static IRollBuilderWithDie WithDie(Die die)
        {
            return new RollBuilder(die);
        }

        public IRollBuilderWithDie WithNumbers(INumberGenerator numberGenerator)
        {
            NumberGenerator = numberGenerator;
            return this;
        }
    }
}