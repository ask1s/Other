using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Strategies
{
    public interface ISpeedStrategy
    {
        public double GetSpeed();

        public int GetEffectTime();
    }
}
