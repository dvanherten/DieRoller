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

        public decimal Probability => _requiredRollOrHigher / 6m;
    }
}
