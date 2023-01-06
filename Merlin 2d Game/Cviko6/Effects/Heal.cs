using Cviko6.Actors;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Effects
{
    public class Heal : Command
    {
        private Player player;
        private int heal;
        public Heal(Player player, int heal)
        {
            this.player = player;
            this.heal = heal;
        }

        public void Execute()
        {
            player.ChangeHealth(heal);
        }
    }
}
