namespace DieRoller
{
    /// <summary>
    /// Fluent builder to aid in the creation of a valid <see cref="Roll"/>.
    /// </summary>
    public class RollBuilder : IRollBuilderWithDie, IRollBuilderWithTarget, IRollBuilderWithReroll, IBuildableRoll
    {
        private readonly Die _die;
        private IRerollBehaviour _rerollOptions = Reroll.None;
        private IRollModifier _rollModifier = new NoModifier();
        private IRollTarget _target;

        private RollBuilder(Die die)
        {
            _die = die;
        }

        public IRollBuilderWithTarget Targeting(IRollTarget target)
        {
            _target = target;
            return this;
        }

        public IBuildableRoll WithModifier(int modifier)
        {
            _rollModifier = new FlatRollModifier(modifier);
            return this;
        }

        public IRollBuilderWithReroll WithReroll(IRerollBehaviour rerollOptions)
        {
            _rerollOptions = rerollOptions;
            return this;
        }

        public Roll Build()
        {
            return new Roll(_die, _target, _rerollOptions, _rollModifier);
        }

        public static IRollBuilderWithDie WithDie(Die die)
        {
            return new RollBuilder(die);
        }
    }
}