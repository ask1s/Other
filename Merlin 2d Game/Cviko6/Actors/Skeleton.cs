using System;
using System.Collections.Generic;
using System.Text;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Cviko6.Commands;
using Cviko6.Strategies;
using Merlin2d.Game.Enums;

namespace Cviko6.Actors
{
    public class Skeleton : AbstractCharacter, IMovable
    {
        
        private Animation animation = new Animation("resources/sprites/enemy.png", 64, 58);

        private int time = 0;
        private int timer = 0;

        private Command moveLeft;
        private Command moveRight;
        private Command moveUp;

        MessageDuration md = (MessageDuration)240;

        protected bool inJump = false;

        private bool toRight = true;
        private bool toLeft = false;

        private int posX;

        private int vision = 100;

        private double speed = 1;

        private int timer1 = 0;
        private int timer2 = 0;

        private int x;
        private bool inRange = false;

        public bool d = false;
        public int dam = 0;
        public int enemys = 0;

        private int orientation = (int)ActorOrientation.Right;

        public ISpeedStrategy speedStrategy;

        private Random random = new Random();
        public Skeleton(int enemys)
        {
            SetPhysics(true);
            SetAnimation(animation);
            animation.Start();

            speed = GetSpeed();

            if(enemys == 0)
                SetPosition(414, 613);
            else if(enemys == 1)
                SetPosition(651, 603);
            else
                SetPosition(987, 629);

            speedStrategy = new NormalSpeedStrategy(2);

            SetName("Enemy");

        }

        public void dot(int damage)
        {
            ChangeHealth(damage);
        }

        public override void Update()
        {

            posX = GetWorld().GetActors().Find(a => a.GetName() == "Player").GetX();

            speed = GetSpeed();
            moveLeft = new Move(this, speed, -1, 0);
            moveRight = new Move(this, speed, 1, 0);

            if (speed != 2)
            {
                timer++;
                if (timer == speedStrategy.GetEffectTime()*60)
                {
                    SetSpeedStrategy(new NormalSpeedStrategy(2));
                    speedStrategy = new NormalSpeedStrategy(2);
                    timer = 0;
                }
            }

            if(d)
            {
                timer1++;
                if (timer1 % 60 == 0)
                    dot(dam);
                if (timer1 == 5 * 60)
                {
                    timer1 = 0;
                    d = false;
                }
            }


            time++;
            if (time % 120 == 0)
            {
                x = random.Next(0, 10);
            }

            if(this.GetX() < posX)
            {
                orientation = 1;
                if (posX - this.GetX() < vision)
                    inRange = true;
                else
                    inRange = false;
            }
            else
            {
                orientation = 0;
                if (this.GetX() - posX < vision)
                    inRange = true;
                else
                    inRange = false;
            }

            if(inRange == true)
            {
                if(orientation == 0)
                {
                    if (toLeft == true && toRight == false)
                    {
                        moveLeft.Execute();
                        animation.Start();
                    }
                    else
                    {
                        toRight = false;
                        toLeft = true;

                        animation.Stop();
                        animation.FlipAnimation();

                        moveLeft.Execute();
                        animation.Start();

                    }
                }
                else
                {
                    if (toRight == true && toLeft == false)
                    {
                        moveRight.Execute();
                        animation.Start();
                    }
                    else
                    {
                        toRight = true;
                        toLeft = false;

                        animation.Stop();
                        animation.FlipAnimation();

                        moveRight.Execute();
                        animation.Start();

                    }
                }
            }
            else if (x >= 5)
            {
                if (toRight == true && toLeft == false)
                {
                    moveRight.Execute();
                    animation.Start();
                }
                else
                {
                    toRight = true;
                    toLeft = false;

                    animation.Stop();
                    animation.FlipAnimation();

                    moveRight.Execute();
                    animation.Start();

                }
            }
            else
            {
                if (toLeft == true && toRight == false)
                {
                    moveLeft.Execute();
                    animation.Start();
                }
                else
                {
                    toRight = false;
                    toLeft = true;

                    animation.Stop();
                    animation.FlipAnimation();

                    moveLeft.Execute();
                    animation.Start();
                    
                }
            }
            if (GetHealth() <= 0)
                Die();
        }
        public override void Die()
        {
            Message msg = new Message("Killed", GetX() + 5, GetY() - 5, default, Color.Red, md);
            GetWorld().AddMessage(msg);

            GetWorld().RemoveActor(this);
        }
    }
}
