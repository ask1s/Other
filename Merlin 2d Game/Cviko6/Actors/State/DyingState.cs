using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Actors.State
{
    public class DyingState : IPlayerState
    {
        private Player player;
        private double speed = 0;

        private Animation animation = new Animation("resources/sprites/player.png", 64, 58);
        public DyingState(Player player)
        {
            this.player = player;
            Message message = new Message("Game over. You died", player.GetX(), player.GetY()-100);
            player.GetWorld().AddMessage(message);
        }
        public Animation GetAnimation()
        {
            return animation;
        }

        public void Update()
        {

        }
    }
}
