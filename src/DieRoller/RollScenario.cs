using System;

namespace DieRoller
{
    public class Roll
    {
        private readonly IDie _die;
        private readonly int _requiredRollOrHigher;

        public Roll(IDie die, int requiredRollOrHigher)
        {
            if (die == null) throw new ArgumentNullException(nameof(die));
            if (requiredRollOrHigher <= 0) throw new ArgumentOutOfRangeException(nameof(requiredRollOrHigher));

            _die = die;
            _requiredRollOrHigher = requiredRollOrHigher;
        }

        public decimal Probability
        {
            get
            {
                if (_die.Sides < 1)
                    return 0;
                return (_die.Sides + 1 - _requiredRollOrHigher) / (decimal) _die.Sides;
            }
        }
    }
}
