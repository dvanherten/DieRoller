namespace DieRoller
{
    /// <summary>
    /// Fluent builder to aid in the creation of a valid <see cref="Roll"/>.
    /// </summary>
    public class RollBuilder : IRollBuilderWithDie, IRollBuilderWithTarget, IRollBuilderWithReroll, IBuildableRoll
    {
        private readonly IDie _die;
        private IRerollOptions _rerollOptions = RerollOptions.None;
        private IRollModifier _rollModifier = new NoModifier();
        private IRollTarget _target;

        private RollBuilder(IDie die)
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

        public IRollBuilderWithReroll WithReroll(RerollOptions rerollOptions)
        {
            _rerollOptions = rerollOptions;
            return this;
        }

        public Roll Build()
        {
            return new Roll(_die, _target, _rerollOptions, _rollModifier);
        }

        public static IRollBuilderWithDie WithDie(DSix d6Die)
        {
            return new RollBuilder(d6Die);
        }
    }
}