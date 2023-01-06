using System;
using System.Collections.Generic;
using System.Text;
using Merlin2d.Game.Actions;
using Cviko6.Actors;
using Merlin2d.Game;

namespace Cviko6.Spells
{
    public interface ISpellBuilder
    {
        ISpellBuilder AddEffect(string effectName);
        ISpellBuilder SetAnimation(Animation animation);
        ISpellBuilder SetSpellCost(int cost);
        ISpell CreateSpell(IWizard caster);
    }
}
