using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Actors;
using Cviko6.Strategies;
using Merlin2d.Game.Actors;

namespace Cviko6.Commands
{
    public interface IMovable
    {
        void SetSpeedStrategy(ISpeedStrategy speedStrategy);
        double GetSpeed();

    }
}
