using System;
using System.Collections.Generic;
using System.Text;
using Cviko6.Spells;
using Merlin2d.Game.Actors;

namespace Cviko6.Actors
{
    public interface IWizard : IActor
    {
        public void ChangeMana(int delta);
        public int GetMana();
        public void Cast(ISpell spell);
        public ISpell CreateSpell(string name);

    }

}
