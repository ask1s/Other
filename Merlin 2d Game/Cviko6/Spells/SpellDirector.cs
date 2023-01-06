using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Actors;
using Merlin2d.Game;

namespace Cviko6.Spells
{
    public class SpellDirector : ISpellDirector
    {
        Dictionary<string, SpellInfo> spells;
        Dictionary<string, int> effectCosts;
        private IWizard wizard;
        private int costs = 0;
        private SpellInfo info;

        public SpellDirector(IWizard wizard, ISpellDataProvider provider)
        {
            this.wizard = wizard;
            spells = provider.GetSpellInfo();
            effectCosts = provider.GetSpellEffects();
        }
        public ISpell Build(string spellName)
        {
            ISpellBuilder builder;
            string[] value;

            if (spells[spellName].SpellType == SpellType.Projectile)
            {
                int AW = Convert.ToInt32(spells[spellName].AnimationWidth);
                int AH = Convert.ToInt32(spells[spellName].AnimationHeight);
                string AP = spells[spellName].AnimationPath;

                Animation animation = new Animation(AP, AW, AH);

                builder = new ProjectileSpellBuilder(wizard);
                builder.SetAnimation(animation);
            }
            else
            {
                builder = new SelfCastSpellBuilder(wizard);
            }
            
            foreach(string name in spells[spellName].EffectNames)
            {
                value = name.Split(" ");
                builder.AddEffect(name);

                if(effectCosts[value[0]] > 0)
                {
                    costs += effectCosts[value[0]];
                }
            }

            builder.SetSpellCost(costs);
            return builder.CreateSpell(wizard);
        }
    }
}
