using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Actors;
using Merlin2d.Game.Actors;

namespace Cviko6.Effects
{
    public class Damage : Command
    {
        private Skeleton enemy;

        private int damage;

        public Damage(Skeleton enemy, int damage)
        {
            this.enemy = enemy;
            this.damage = damage;
        }

        public void Execute()
        {
            enemy.ChangeHealth(damage);
        }
    }
}
