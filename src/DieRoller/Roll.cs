using System;
using System.Linq;
using Ardalis.GuardClauses;

namespace DieRoller
{
    public class Roll
    {
        private readonly Die _die;
        private readonly IRollTarget _target;
        private readonly IRerollBehaviour _rerollOptions;
        private readonly IRollModifier _modifier;
        private readonly INumberGenerator _numberGenerator;

        internal Roll(Die die, IRollTarget target, IRerollBehaviour rerollOptions, IRollModifier modifier, INumberGenerator numberGenerator)
        {
            Guard.Against.Null(die, nameof(die));
            Guard.Against.Null(rerollOptions, nameof(rerollOptions));
            Guard.Against.Null(rerollOptions, nameof(rerollOptions));
            Guard.Against.Null(modifier, nameof(modifier));
            Guard.Against.Null(numberGenerator, nameof(numberGenerator));

            _die = die;
            _target = target;
            _rerollOptions = rerollOptions;
            _modifier = modifier;
            _numberGenerator = numberGenerator;
        }

        public decimal CalculateProbability()
        {
            var successfulSides = _target.GetModifiedSuccessfulSides(_die.TotalSides, _modifier).ToArray();
            var rerollSides = _rerollOptions.GetRerollSides(_die, _target).ToArray();
            var initialSuccessfulSides = successfulSides.Except(rerollSides);
            var baseProbability = _die.CalculateProbability(initialSuccessfulSides.Count());
            var rerollProbability = _rerollOptions.CalculateProbability(_die, _target, _modifier);
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

            var modifiedValue = _modifier.ModifyRoll(final.SideRolled);

            return new RollResult(_target, initial, rerollResult, _modifier.ModifierValue, modifiedValue);
        }
    }
}
