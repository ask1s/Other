using Merlin2d.Game;
using Merlin2d.Game.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Actors.Items
{
    public class HealingPotion : AbstractActor, IItem, IUsable
    {
        private int usesRemaining = 1;
        private Player player;

        public HealingPotion(Player player)
        {
            SetAnimation(new Animation("resources/sprites/healing.png",60,74));
            this.player = player;
        }
        public override void Update()
        {
        }

        public void Use(ICharacter character)
        {
            if(usesRemaining-- > 0)
            {
                player.ChangeHealth(-40);
            }
        }
    }
}
