using System;

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
            _numberGenerator = numberGenerator;
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

        public int Simulate()
        {
            return _die.Simulate(_numberGenerator);
        }
    }

    /// <summary>
    /// Implementation using random to be used at runtime.
    /// </summary>
    public class RandomNumberGenerator : INumberGenerator
    {
        public static Random Random = new Random();
        public int GetNumber()
        {
            return Random.Next();
        }
    }

    /// <summary>
    /// Interface to abstract Random for testing purposes.
    /// </summary>
    public interface INumberGenerator
    {
        int GetNumber();
    }
}
