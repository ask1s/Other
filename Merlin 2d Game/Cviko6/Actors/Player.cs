using Cviko6.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using Cviko6.Spells;
using Cviko6.Strategies;
using Cviko6.Actors.State;
using Cviko6.Actors.Items;
using Merlin2d.Game.Enums;

namespace Cviko6.Actors
{
    public class Player : AbstractCharacter, IMovable, IWizard
    {
        public IPlayerState state;

        private int mana = 1000;
        private ISpell spell;
        public ISpeedStrategy speedStrategy;

        private int timer1 = 0;
        public int dam = 5;
        public bool d = false;

        MessageDuration md = (MessageDuration)120;

        public Backpack backpack;

        public int orientation = (int)ActorOrientation.Right;
        public Player()
        {
            state = new LivingState(this);

            SetPhysics(true);

            speedStrategy = new NormalSpeedStrategy(2);

            SetSpeedStrategy(speedStrategy);

            SetAnimation(GetAnimation());

            SetName("Player");

            SetPosition(87, 591);

            backpack = new Backpack(5);
            backpack.AddItem(new HealingPotion(this));
            backpack.AddItem(new ManaPotion(this));
            backpack.AddItem(new HealingPotion(this));
            backpack.AddItem(new ManaPotion(this));
        }

        public override Animation GetAnimation()
        {
            return state.GetAnimation();
        }
        public override void Update()
        {
            base.Update();
            state.Update();
            if (d)
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

            SetAnimation(GetAnimation());
        }

        public void dot(int damage)
        {
            ChangeHealth(damage);
        }

        public void ChangeMana(int delta)
        {
            if (delta > 0)
            {
                mana -= delta;
                delta = delta * -1;
            }
            else
            {
                delta = delta * -1;
                mana += delta;
            }
            Message msg = new Message(Convert.ToString(delta), GetX()+5, GetY() - 5, default, Color.Blue, md);
            GetWorld().AddMessage(msg);
        }

        public int GetMana()
        {
            return mana;
        }

        public ISpell CreateSpell(string name)
        {
            ISpellDirector director = new SpellDirector(this, SpellDataProvider.GetInstance());
            return director.Build(name);
        }
        public void Cast(ISpell spell)
        {
            if(mana >= spell.GetCost())
            {
                ChangeMana(spell.GetCost());
                spell.Cast();
            }
            else
            {

            }
        }
        public override void Die()
        {
            state = new DyingState(this);
        }
    }
}
