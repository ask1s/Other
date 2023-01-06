using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Actors.State
{
    public interface IPlayerState
    {
        public void Update();

        public Animation GetAnimation();
    }
}
