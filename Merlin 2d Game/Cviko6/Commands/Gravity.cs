using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cviko6.Actors;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;

namespace Cviko6.Commands
{
    public class Gravity : IPhysics
    {
        private IWorld world;
        private IAction<IActor> fall = new Fall<IActor>(2);

        public void SetWorld(IWorld world)
        {
            this.world = world;
        }

        public void Execute()
        {   
            world.GetActors().Where(a => a.IsAffectedByPhysics()).ToList().ForEach(a => fall.Execute(a));
        }
    }
}
