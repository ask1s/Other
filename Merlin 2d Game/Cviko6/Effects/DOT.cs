using Cviko6.Actors;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Effects
{
    public class DOT : Command
    {
        private Skeleton enemy;
        private Player player;

        private int damage;
       

        public DOT(Skeleton enemy, int damage)
        {
            this.enemy = enemy;
            this.damage = damage;
        }
        public DOT(Player player, int damage)
        {
            this.player = player;
            this.damage = damage;
        }

        public void Execute()
        {
            if (player == null)
            {
                enemy.d = true;
                enemy.dam = damage;
            }
            else
            {
                player.d = true;
                player.dam = damage;
            }

        }
    }
}
