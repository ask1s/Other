using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Merlin2d.Game;
using Cviko6.Spells;
using Cviko6.Actors;

namespace Cviko6.Commands
{
    public class Move : Command
    {
        private IActor actor;
        private int dx;
        private int dy;
        private double speed;
        private IWorld world;
        public Move(IActor actor, double speed, int dx, int dy)
        {
            if (actor != null)
            {
                this.actor = actor;
                this.dx = dx;
                this.dy = dy;
                this.speed = speed;
                world = actor.GetWorld();
            }
        }
        
        public void Execute()
        {
            actor.SetPosition(actor.GetX() + dx * (int)speed, actor.GetY() + dy * (int)speed);
            if (world != null)
            {
                if (actor.GetWorld().IntersectWithWall(actor))
                {
                    if (dx != 0 && dy == 0)
                    {
                        actor.SetPosition(actor.GetX() - dx * (int)speed, actor.GetY());
                    }
                    else if (dy != 0 && dx == 0)
                    {
                        actor.SetPosition(actor.GetX(), actor.GetY() - dy * (int)speed);
                    }
                }
            }
        }
       
    }
}
