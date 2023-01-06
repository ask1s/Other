using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Actors;

namespace Cviko6.Strategies
{
    public class NormalSpeedStrategy : ISpeedStrategy
    {
        private double speed;

        public NormalSpeedStrategy(double speed)
        {
            this.speed = speed;
        }
        public double GetSpeed()
        {
            speed = 2;
            return speed;
        }
        public int GetEffectTime()
        {
            return 0;
        }
    }
}
