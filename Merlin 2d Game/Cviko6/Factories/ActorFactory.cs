using System;
using System.Collections.Generic;
using System.Text;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Cviko6.Actors;

namespace Cviko6.Factories
{
    public class ActorFactory : IFactory
    {
        public int enemys = -1;
        public int anime = -1;
        public IActor Create(string actorType, string actorName, int x, int y)
        {
            IActor actor;
            

            if (actorType == "Player")
                return new Player();
            else if (actorType == "Enemy")
            {
                enemys++;
                return new Skeleton(enemys);
            }
            else if(actorType == "light")
            {
                anime++;
                return new Light(anime);
            }

            return null;
        }
    }
}
