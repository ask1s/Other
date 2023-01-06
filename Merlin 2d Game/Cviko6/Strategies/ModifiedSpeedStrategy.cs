using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Strategies
{
    public class ModifiedSpeedStrategy : ISpeedStrategy
    {
        private double speedMultiplier;
        private int time;
        private int speed;

        public ModifiedSpeedStrategy(double speedMultiplier, int time)
        {
            this.speedMultiplier = speedMultiplier;
            speed = 2;
            this.time = time;
        }

        public int GetEffectTime()
        {
            return time;
        }

        public double GetSpeed()
        {
            return speedMultiplier * speed;
        }
        
    }
}
