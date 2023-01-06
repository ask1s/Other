using Cviko6.Actors;
using Cviko6.Effects;
using Merlin2d.Game;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Spells
{
    public class ProjectileSpellBuilder : ISpellBuilder
    {
        private int cost;
        private List<Command> effects = new List<Command>();
        private IWizard wizard;

        private Command damage;
        private Command dot;
        private Command slow;

        private Player player;
        private Skeleton enemy;


        private Animation animation;
        public ProjectileSpellBuilder(IWizard wizard)
        {
            this.wizard = wizard;
            player = (Player)wizard;
            enemy = (Skeleton)player.GetWorld().GetActors().Find(a => a.GetName() == "Enemy");
        }
        public ISpellBuilder AddEffect(string effectName)
        {
            effects.Add(CreateEffect(effectName));
            return this;
        }
        private Command CreateEffect(string name)
        {
            string[] value = name.Split(" ");

            if (value[0] == "damage")
            {
                damage = new Damage(enemy, Convert.ToInt32(value[1]));
                return damage;
            }
            else if (value[0] == "dot")
            {
                dot = new DOT(enemy, Convert.ToInt32(value[1]));
                return dot;
            }
            else if (value[0] == "slow")
            {
                value[1] = value[1].Replace(".", ",");
                slow = new Slow(enemy, Convert.ToDouble(value[1]));
                return slow;
            }

            else
                return null;
        }

        public ISpell CreateSpell(IWizard wizard)
        {
            ISpell spell = new ProjectileSpell(wizard, cost, animation);

            spell.AddEffects(effects);
            return spell;
        }

        public ISpellBuilder SetAnimation(Animation animation)
        {
            this.animation = animation;
            return this;
        }

        public ISpellBuilder SetSpellCost(int cost)
        {
            this.cost = cost;
            return this;
        }
    }
}
