using System;

namespace DieRoller
{
    public class Roll
    {
        private readonly Die _die;
        private readonly IRollTarget _target;
        private readonly IRerollBehaviour _rerollOptions;
        private readonly IRollModifier _modifier;

        internal Roll(Die die, IRollTarget target, IRerollBehaviour rerollOptions, IRollModifier modifier)
        {
            _die = die ?? throw new ArgumentNullException(nameof(die));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _rerollOptions = rerollOptions ?? throw new ArgumentNullException(nameof(rerollOptions));
            _modifier = modifier ?? throw new ArgumentNullException(nameof(modifier));
        }

        public decimal CalculateProbability()
        {
            if (_die.Sides < 1)
                return 0;
            var successfulSideCount = _target.GetSuccessCount(_die.Sides);
            var baseProbability = _die.CalculateProbability(successfulSideCount);
            var rerollProbability = _rerollOptions.CalculateProbability(_die, _target);
            return baseProbability + rerollProbability;
        }
    }

    public class RollResult
    {
    }
}
