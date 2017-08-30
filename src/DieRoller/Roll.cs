using System;
using System.Linq;

namespace DieRoller
{
    public class Roll
    {
        private readonly Die _die;
        private readonly IRollTarget _target;
        private readonly IRerollBehaviour _rerollOptions;
        private readonly INumberGenerator _numberGenerator;

        internal Roll(Die die, IRollTarget target, IRerollBehaviour rerollOptions, INumberGenerator numberGenerator)
        {
            _numberGenerator = numberGenerator;
            _die = die ?? throw new ArgumentNullException(nameof(die));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _rerollOptions = rerollOptions ?? throw new ArgumentNullException(nameof(rerollOptions));
        }

        public decimal CalculateProbability()
        {
            var successfulSides = _target.GetModifiedSuccessfulSides(_die.TotalSides).ToArray();
            var rerollSides = _rerollOptions.GetRerollSides(_die, _target).ToArray();
            var initialSuccessfulSides = successfulSides.Except(rerollSides);
            var baseProbability = _die.CalculateProbability(initialSuccessfulSides.Count());
            var rerollProbability = _rerollOptions.CalculateProbability(_die, _target);
            return baseProbability + rerollProbability;
        }

        public RollResult Simulate()
        {
            var initial = _die.Simulate(_numberGenerator);
            SingleRollResult rerollResult = null;
            var final = initial;

            if (_rerollOptions.RequiresReroll(initial, _target))
            {
                rerollResult = _die.Simulate(_numberGenerator);
                final = rerollResult;
            }

            return new RollResult(_target, initial, rerollResult, final.SideRolled);
        }
    }
}
