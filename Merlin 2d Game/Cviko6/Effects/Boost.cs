using Cviko6.Actors;
using Merlin2d.Game.Actions;
using Cviko6.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Effects
{
    public class Boost : Command
    {
        private Player player;
        private double power;

        public Boost(Player player, double power)
        {
            this.player = player;
            this.power = power;
        }
        public void Execute()
        {
            player.SetSpeedStrategy(new ModifiedSpeedStrategy(power, 10));
            player.speedStrategy = new ModifiedSpeedStrategy(power, 10);
        }
    }
}
