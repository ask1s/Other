using Cviko6.Actors.Items;
using Cviko6.Commands;
using Cviko6.Strategies;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Items;
using System;
using System.Collections.Generic;
using System.Text;
using Merlin2d.Game.Enums;

namespace Cviko6.Actors.State
{
    class LivingState : IPlayerState
    {
        private Command moveLeft;
        private Command moveRight;
        private Command moveUp;

        private Command DamageLeft;
        private Command DamageRight;

        private bool moving = false;
        private bool inJump = false;
        private int jumps = 0;

        MessageDuration md = (MessageDuration)2;

        private double speed = 1;

        private int timer = 0;
        

        private Player player;

        private Animation animation = new Animation("resources/sprites/player.png", 64, 58);

        public LivingState(Player player)
        {
            this.player = player;
            animation.Stop();

            speed = this.player.GetSpeed();

            this.player.SetAnimation(animation);

            DamageLeft = new Move(this.player, speed, -40, 0);
            DamageRight = new Move(this.player, speed, 40, 0);
        }

        public Animation GetAnimation()
        {
            return animation;
        }
        

        public void Update()
        {
            Message msg1 = new Message("Health: "+ Convert.ToString(player.GetHealth()), 592, 192, 20, Color.Green, md);
            Message msg2 = new Message("Mana: " + Convert.ToString(player.GetMana()), 592, 224, 20, Color.Blue, md);
            player.GetWorld().AddMessage(msg1);
            player.GetWorld().AddMessage(msg2);

            speed = player.GetSpeed();

            player.GetWorld().ShowInventory(player.backpack);

            moveLeft = new Move(this.player, speed, -1, 0);
            moveRight = new Move(this.player, speed, 1, 0);
            moveUp = new Move(this.player, 1, 0, -40);

            if (player.IntersectsWithActor(player.GetWorld().GetActors().Find(a => a.GetName() == "Enemy")))
            {
                moveUp.Execute();
                moveUp.Execute();
                if (player.GetX() > player.GetWorld().GetActors().Find(a => a.GetName() == "Enemy").GetX())
                    DamageRight.Execute();
                else
                    DamageLeft.Execute();

                player.ChangeHealth(25);
            }

            if (speed != 2)
            {
                timer++;
                if (timer == player.speedStrategy.GetEffectTime()*60)
                {
                    player.SetSpeedStrategy(new NormalSpeedStrategy(2));
                    player.speedStrategy = new NormalSpeedStrategy(2);
                    timer = 0;
                }
            }

            if (Input.GetInstance().IsKeyPressed(Input.Key.Z))
            {
                player.Cast(player.CreateSpell("fireball"));
            }

            if (Input.GetInstance().IsKeyPressed(Input.Key.X))
            {
                player.Cast(player.CreateSpell("frostball"));
            }
            if (Input.GetInstance().IsKeyPressed(Input.Key.C))
            {
               player.Cast(player.CreateSpell("heal"));
            }
            if (Input.GetInstance().IsKeyPressed(Input.Key.V))
            {
                player.Cast(player.CreateSpell("boost"));
            }
            if (Input.GetInstance().IsKeyPressed(Input.Key.S))
            {
                IUsable item = (IUsable)player.backpack.GetItem();
                item.Use(player);
            }
            if (Input.GetInstance().IsKeyPressed(Input.Key.A))
            {
                player.backpack.ShiftLeft();
            }
            if (Input.GetInstance().IsKeyPressed(Input.Key.D))
            {
                player.backpack.ShiftRight();
            }


            if (Input.GetInstance().IsKeyDown(Input.Key.LEFT))
            {
                if (player.orientation == 0)
                {
                    moveLeft.Execute();
                    animation.Start();
                    moving = true;
                }
                else
                {
                    player.orientation = 0;

                    animation.Stop();
                    animation.SetCurrentFrame(0);
                    animation.FlipAnimation();

                    moveLeft.Execute();
                    animation.Start();
                    moving = true;
                }
            }
            else if (Input.GetInstance().IsKeyDown(Input.Key.RIGHT))
            {
                if (player.orientation == 1)
                {
                    moveRight.Execute();
                    animation.Start();
                    moving = true;
                }
                else
                {
                    player.orientation = 1;

                    animation.Stop();
                    animation.SetCurrentFrame(0);
                    animation.FlipAnimation();

                    moveRight.Execute();
                    animation.Start();
                    moving = true;
                }
            }
            else
                moving = false;

            if (Input.GetInstance().IsKeyPressed(Input.Key.SPACE) || inJump == true)
            {
                player.SetPosition(player.GetX(), player.GetY() + 1);
                if (player.GetWorld().IntersectWithWall(player))
                    inJump = true;
                player.SetPosition(player.GetX(), player.GetY() - 1);

                if (inJump == true && jumps < 3)
                {
                    moveUp.Execute();
                    jumps++;
                }
                else if (jumps == 3)
                {
                    jumps = 0;
                    inJump = false;
                }
            }

            if (!moving)
            {
                animation.Stop();
                animation.SetCurrentFrame(0);
            }
            else
                animation.Start();
        }
    }
}
