using Cviko6.Actors;
using Cviko6.Commands;
using Cviko6.Effects;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Spells
{
    public class SelfCastSpellBuilder : ISpellBuilder
    {
        private int cost;
        private IWizard wizard;
        private List<Command> effects = new List<Command>();

        private Command heal;
        private Command boost;
        private Command dot_self;

       

        public SelfCastSpellBuilder(IWizard wizard)
        {
            this.wizard = wizard;
        }
        public ISpellBuilder AddEffect(string effectName)
        {
            effects.Add(CreateEffect(effectName));
            return this;
        }

        private Command CreateEffect(string effectName)
        {
            string[] value = effectName.Split(" ");

            if (value[0] == "heal")
            {
                heal = new Heal((Player)wizard, Convert.ToInt32(value[1]));
                return heal;
            }
            else if (value[0] == "boost")
            {
                value[1] = value[1].Replace(".", ",");
                boost = new Boost((Player)wizard, Convert.ToDouble(value[1]));
                return boost;
            }
            else if(value[0]== "dot_self")
            {
                dot_self = new dot_self_((Player)wizard, Convert.ToInt32(value[1]));
                return dot_self;
            }
            else
                return null;
        }

        public ISpell CreateSpell(IWizard wizard)
        {
            ISpell spell = new SelfCastSpell(wizard, cost);

            spell.AddEffects(effects);
            return spell;
        }

        public ISpellBuilder SetAnimation(Animation animation)
        {
            throw new ArgumentException();
        }

        public ISpellBuilder SetSpellCost(int cost)
        {
            this.cost = cost;
            return this;
        }

    }
}
