using System;
using System.Collections.Generic;
using System.Text;
using Merlin2d.Game.Actions;
using Cviko6.Strategies;
using Merlin2d.Game;
using Merlin2d.Game.Enums;

namespace Cviko6.Actors
{
    public abstract class AbstractCharacter : AbstractActor, ICharacter
    {
        private List<Command> effects = new List<Command>();
        private int health = 100;
        private double speed = 1;
        private ISpeedStrategy speedStrategy;
        MessageDuration md = (MessageDuration)120;

        public AbstractCharacter()
        {
            speedStrategy = new NormalSpeedStrategy(2);
            speed = GetSpeed();
        }

        public void AddEffect(Command effect)
        {
            effects.Add(effect);
        }

        public void ChangeHealth(int delta)
        {
            if (delta < 0)
            {
                delta = delta * -1;
                health = health + delta;
            }
            else
            {
                health -= delta;
                delta = delta * -1;
            }

            if (health <= 0)
                Die();

            if (health > 100)
                health = 100;

            Message msg = new Message(Convert.ToString(delta), GetX()+5, GetY()-5, default,Color.Red,md);
            GetWorld().AddMessage(msg);
        }

        public virtual void Die()
        {
            //Die things
        }

        public int GetHealth()
        {
            return health;
        }

        public void RemoveEffect(Command effect)
        {
            effects.Remove(effect);
        }
        public override void Update()
        {
            effects.ForEach(e => e.Execute());
        }

        public void SetSpeedStrategy(ISpeedStrategy speedStrategy)
        {
            this.speedStrategy = speedStrategy;
        }

        public double GetSpeed()
        {
            return speedStrategy.GetSpeed();
        }
    }
}
