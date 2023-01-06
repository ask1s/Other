using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Actors;

namespace Cviko6.Strategies
{
    public class LimitedSpeedStrategy : ISpeedStrategy
    {
        private int speed = 2;
        public LimitedSpeedStrategy()
        {
            speed = 2;
        }
        public double GetSpeed()
        {
            return speed < 1 ? speed : 1;
        }
        public int GetEffectTime()
        {
            return 0;
        }
    }
}
