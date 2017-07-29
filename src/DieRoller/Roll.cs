using System;

namespace DieRoller
{
    public class Roll
    {
        private readonly IDie _die;
        private readonly IRollTarget _target;
        private readonly IRerollOptions _rerollOptions;
        private readonly IRollModifier _modifier;

        internal Roll(IDie die, IRollTarget target, IRerollOptions rerollOptions, IRollModifier modifier)
        {
            _die = die ?? throw new ArgumentNullException(nameof(die));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _rerollOptions = rerollOptions ?? throw new ArgumentNullException(nameof(rerollOptions));
            _modifier = modifier ?? throw new ArgumentNullException(nameof(modifier));
        }

        public decimal Probability
        {
            get
            {
                if (_die.Sides < 1)
                    return 0;
                var possibleSuccesses = _target.GetSuccessCount(_die.Sides);
                var baseProbability = possibleSuccesses / (decimal)_die.Sides;
                return baseProbability;
            }
        }
    }

    public class RollResult
    {
    }
}
