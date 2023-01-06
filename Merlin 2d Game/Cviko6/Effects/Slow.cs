using Cviko6.Actors;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Strategies;

namespace Cviko6.Effects
{
    public class Slow : Command
    {
        private Skeleton enemy;
        private double power;

        public Slow(Skeleton enemy, double power)
        {
            this.enemy = enemy;
            this.power = power;
        }
        public void Execute()
        {
            enemy.SetSpeedStrategy(new ModifiedSpeedStrategy(power, 10));
            enemy.speedStrategy = new ModifiedSpeedStrategy(power, 10);
        }
    }
}
