using Cviko6.Actors;
using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cviko6.Spells
{
    public class SelfCastSpell : ISpell
    {
        private IEnumerable<Command> effects = new List<Command>();
        private List<Command> effect = new List<Command>();
        private int cost;
        private IWizard wizard;
        public SelfCastSpell(IWizard wizard, int cost)
        {
            this.wizard = wizard;
            this.cost = cost;
        }
        public void AddEffect(Command effect)
        {
            this.effect.Add(effect);
        }

        public void AddEffects(IEnumerable<Command> effects)
        {
            this.effects = effects;
        }

        public void Cast()
        {
            foreach(Command effect in effects)
            {
                effect.Execute();
            }
        }

        public int GetCost()
        {
            return cost;
        }
    }
}
