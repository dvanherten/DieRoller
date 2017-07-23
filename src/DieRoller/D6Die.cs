using System;

namespace DieRoller
{
    public class D6Die
    {
        private readonly int _requiredRollOrHigher;

        public D6Die(int requiredRollOrHigher)
        {
            _requiredRollOrHigher = requiredRollOrHigher;
        }

        public decimal Probability => (7 - _requiredRollOrHigher) / 6m;
    }
}
