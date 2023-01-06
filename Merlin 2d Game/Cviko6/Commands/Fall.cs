using System;
using System.Collections.Generic;
using System.Text;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Actions;

namespace Cviko6.Commands
{
    public class Fall<T> : IAction<T> where T : IActor
    {
        private int FS = 2;
        private int counter = 0;
        private int multiplier = 1;
        public Fall(int FS)
        {
            this.FS = FS;
        }
        public void Execute(T t)
        {
            if (counter % 5 == 0)
                multiplier++;
            t.SetPosition(t.GetX(), t.GetY() + (FS*multiplier));
            if(t.GetWorld().IntersectWithWall(t))
            {
                while (t.GetWorld().IntersectWithWall(t))
                    t.SetPosition(t.GetX(), t.GetY() - 1);
                multiplier = 1;
            }
            counter++;
        }
    }
}
